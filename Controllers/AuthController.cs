using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Vega.API.Data;
using Vega.API.Dtos;
using Vega.API.Models;

namespace Vega.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository authRepository, IMapper mapper, IConfiguration configuration)
        {
            _repo = authRepository;
            _mapper = mapper;
            _config = configuration;
        }

        // Post: api/[controller]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var user = await _repo.UserExist(userDto.Name);
            if (user == true)
                return BadRequest("User already exists with this name. Please choose different name.");

            var newUser = _mapper.Map<User>(userDto);
            
            var storeUser = await _repo.Register(newUser, userDto.Password);
            if (storeUser == null)
                return BadRequest("Something went wrong!");

            var returnNewUser = _mapper.Map<ReturnUserDto>(storeUser);

            return Created("api/auth/" + newUser.Id, returnNewUser);
        }

        // Post: api/[controller]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto credentials)
        {
            var user = await _repo.UserExist(credentials.Name);
            if (user == false)
                return Unauthorized();

            var loginUser = await _repo.Login(credentials.Name.ToLower(), credentials.Password);
            if (loginUser == null)
                return BadRequest("Username or password is not correct!");

            var returnLoggedInUser = _mapper.Map<ReturnUserDto>(loginUser);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loginUser.Id.ToString()),
                new Claim(ClaimTypes.Name, loginUser.Name)
            };

            var key = new SymmetricSecurityKey
                (System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok( new 
            {
                token = tokenHandler.WriteToken(token),
                loggedInUser = returnLoggedInUser
            });
        }
    }
}