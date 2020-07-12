using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vega.API.Data;
using Vega.API.Dtos;

namespace Vega.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelRepository _repo;
        private readonly IMapper _mapper;
        public ModelController(IModelRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        // GET: <controller>/model
        [HttpGet]
        public async Task<IActionResult> GetModels()
        {
            var list = await _repo.GetModels();
            if (list.Equals(null))
                return NotFound("No model registered!");

            var models = _mapper.Map<IEnumerable<ModelDto>>(list);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(int id)
        {
            var model = await _repo.GetModel(id);
            if (model == null)
                return NotFound("No such model exist for vehicle!");

            var vehicleModel = _mapper.Map<ModelDto>(model);
            return Ok(vehicleModel);
        }
    }
}