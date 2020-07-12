using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Data
{
    public interface IManufacturerRepository
    {
        Task<IEnumerable<Manufacturer>> GetManufacturers();
        Task<Manufacturer> GetManufacturer(int id);
    }
}
