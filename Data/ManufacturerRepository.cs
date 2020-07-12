using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Data
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DataContext _ctx;

        public ManufacturerRepository(DataContext context)
        {
            _ctx = context;
        }

        public async Task<Manufacturer> GetManufacturer(int id)
        {
            var manufacturer = await _ctx.Manufacturers.Include(mo => mo.Models).SingleOrDefaultAsync(m => m.Id == id);
            if (manufacturer == null)
                return null;

            return manufacturer;
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturers()
        {
            return await _ctx.Manufacturers.Include(m => m.Models).ToListAsync();
        }
    }
}
