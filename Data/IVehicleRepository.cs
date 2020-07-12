using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Dtos;
using Vega.API.Models;

namespace Vega.API.Data
{
    public interface IVehicleRepository
    {
        Task<bool> IsExist<T>(string name) where T : class;
        Task<IEnumerable<Vehicle>> GetVehicles(int userId);
        Task<Vehicle> GetVehicle(int id, int userId);
        Task<Accessory> GetVehicleAccessories(Vehicle vehicle);
    }
}
