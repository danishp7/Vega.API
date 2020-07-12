using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vega.API.Data;
using Vega.API.Dtos;
using Vega.API.Models;

namespace Vega.API.Controllers
{
    [Authorize]
    [Route("api/Manufacturers")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerRepository _repo;
        private readonly IMapper _mapper;

        public ManufacturerController(IManufacturerRepository manufacturerRepository, IMapper mapper)
        {
            _repo = manufacturerRepository;
            _mapper = mapper;
        }
        // GET: api/manufacturers
        [HttpGet]
        public async Task<IActionResult> GetManufacturers()
        {
            var loggedInUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (loggedInUser == 0)
                return Unauthorized("You are not authorized. Permission denied!");

            var list = await _repo.GetManufacturers();
            if (list == null)
                return NotFound("No manufacturer exists in our system...");

            var manufacturers = _mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ManufacturerDto>>(list);
            return Ok(manufacturers);
        }

        // GET: api/manufacturers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetManufacturer(int id)
        {
            var loggedInUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (loggedInUser == 0)
                return Unauthorized("You are not authorized. Permission denied!");

            var manufacturer = await _repo.GetManufacturer(id);
            if (manufacturer == null)
                return NotFound("No such manufacturer exists in our system...");

            var returnManufacturer = _mapper.Map<ManufacturerDto>(manufacturer);
            return Ok(returnManufacturer);
        }
    }
}