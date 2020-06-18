using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vega.API.Data;
using Vega.API.Dtos;
using Vega.API.Models;

namespace Vega.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _repo;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet("makes")]
        public async Task<IActionResult> Get()
        {
            var list = await _repo.Get();
            var returnList = _mapper.Map<IEnumerable<ManufacturerDto>>(list);
            return Ok(returnList);
        }
    }
}
