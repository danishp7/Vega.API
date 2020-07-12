using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;
using Vega.API.Dtos;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Vega.API.Data
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DataContext _ctx;
        public VehicleRepository(DataContext context)
        {
            _ctx = context;
        }

        public async Task<bool> IsExist<T>(string name) where T : class
        {
            name = name.ToLower(); 
            var vehicle = await _ctx.Vehicles.SingleOrDefaultAsync(v => v.Name == name);
            if (vehicle == null)
                return false;
            return true;
        }

        public async Task<Vehicle> GetVehicle(int id, int userId)
        {
            var vehicle = await _ctx.Vehicles.Include(a => a.Accessories).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle != null && vehicle.UserId == userId)
                return vehicle;
            return null;
        }

        public async Task<Accessory> GetVehicleAccessories(Vehicle vehicle)
        {
            var isVehicle = await _ctx.Vehicles.Include(a => a.Accessories).FirstOrDefaultAsync(v => v.Id == vehicle.Id);
            if (isVehicle == null)
                return null;
            
            var accessoryId = isVehicle.Accessories.FirstOrDefault().AccessoryId;
            var accessories = await _ctx.Accessories.SingleOrDefaultAsync(a => a.Id == accessoryId);

            //var abc = isVehicle.Accessories.SingleOrDefault().Accessory;
            //var def = await _ctx.Accessories.SingleOrDefaultAsync(a => a.Id == abc.AccessoryId);
            return accessories;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles(int userId)
        {
            return await _ctx.Vehicles.Include(a => a.Accessories)
                .Where(u => u.UserId == userId).OrderBy(v => v.Id).ToListAsync();
        }

    }
}
