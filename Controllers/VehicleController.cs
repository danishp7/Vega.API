using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vega.API.Data;
using Vega.API.Dtos;
using Vega.API.Models;

namespace Vega.API.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _repo;
        private readonly IModelRepository _modelRepo;
        private readonly IVegaSharedRepository _vegaRepo;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleRepository repo,
                                 IModelRepository modelRepository,
                                 IVegaSharedRepository vegaSharedRepository,
                                 IMapper mapper)
        {
            _repo = repo;
            _modelRepo = modelRepository;
            _vegaRepo = vegaSharedRepository;
            _mapper = mapper;
        }

    

        // Post: api/<controller>/vehicles
        [HttpPost]
        public async Task<IActionResult> PostVehicle(VehicleDto vehicle)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == 0)
                return Unauthorized("You are not authorize to access.");

            if (!ModelState.IsValid || string.IsNullOrEmpty(vehicle.Name))
                return BadRequest("Something went wrong during post...");

            var model = await _modelRepo.GetModel(vehicle.ModelId);
            if (model == null)
                return BadRequest("Something went wrong with the request...");

            vehicle.Name = vehicle.Name.ToLower();
            var mappedVehicle = _mapper.Map<VehicleDto, Vehicle>(vehicle);

            if (await _repo.IsExist<Vehicle>(mappedVehicle.Name))
                return BadRequest("Vehicle already registered with this name...");

            mappedVehicle.UserId = userId;
            _vegaRepo.Add(mappedVehicle);

            var returnVehicle = _mapper.Map<VehicleDto>(mappedVehicle);

            var accessory = mappedVehicle.Accessories.SingleOrDefault().Accessory;
            var newAccessory = _mapper.Map<Accessory, AccessoryDto>(accessory);
            ReturnVehicleDto newVehicle = new ReturnVehicleDto {
                Name = returnVehicle.Name,
                Price = returnVehicle.Price,
                IsRegistered = returnVehicle.IsRegistered,
                ModelId = returnVehicle.ModelId,
                Accessory = newAccessory
            };

            if (await _vegaRepo.SaveAll())
                return Created("api/vehicles/" + mappedVehicle.Id, newVehicle);

            return BadRequest("something wrong...");
        }

        // Post: api/<controller>/vehicles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, VehicleDto vehicleDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == 0)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong during post...");

            var updateVehicle = await _repo.GetVehicle(id, userId);
            if (updateVehicle == null)
                return BadRequest("No such vehicle exists in our system...");

            var model = await _modelRepo.GetModel(vehicleDto.ModelId);
            if (model == null)
                return BadRequest("No such model exists for this vehicle...");

            _mapper.Map<VehicleDto, Vehicle>(vehicleDto, updateVehicle);

            var getAccessory = vehicleDto.Accessories.SingleOrDefault().Accessory;
            _mapper.Map<Accessory>(getAccessory);

            if(await _vegaRepo.SaveAll())
                return Ok("Vehicle updated Successfully");

            return BadRequest("Something went wrong...");
        }

        // Get: api/<controller>/vehicles
        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == 0)
                return Unauthorized();

            var vehicles = await _repo.GetVehicles(userId);
            if (vehicles == null)
                return NotFound("No such vehicle exists in our system.");

            var returnVehicles = _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
            return Ok(returnVehicles);
        }

        // Get: api/<controller>/vehicles
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == 0)
                return Unauthorized();

            var vehicle = await _repo.GetVehicle(id, userId);
            if (vehicle == null)
                return NotFound("No such vehicle exist in our system!");

            var returnVehicle = _mapper.Map<VehicleDto>(vehicle);
            return Ok(returnVehicle);
        }

        // Get: api/<controller>/vehicles
        [HttpGet("{id}/accessories")]
        public async Task<IActionResult> GetVehicleAccessories(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == 0)
                return Unauthorized();

            var vehicle = await _repo.GetVehicle(id, userId);
            if (vehicle == null)
                return NotFound("No such vehicle exist in our system!");

            var accessories = await _repo.GetVehicleAccessories(vehicle);
            var returnAccessories = _mapper.Map<AccessoryDto>(accessories);
            return Ok(returnAccessories);
        }

        // Delete: api/<controller>/vehicles/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == 0)
                return Unauthorized();

            var vehicle = await _repo.GetVehicle(id, userId);
            if (vehicle == null)
                return BadRequest("No such vehicle exists in our system!");

            _vegaRepo.Delete(vehicle);
            if (await _vegaRepo.SaveAll())
                return Ok("Vehicle has been deleted successfully!");

            return BadRequest("Something went wrong with the request...");
        }

    }
}
