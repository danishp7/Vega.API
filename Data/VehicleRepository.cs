using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Data
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DataContext _ctx;
        public VehicleRepository(DataContext context)
        {
            _ctx = context;
        }

        public async Task<IEnumerable<Manufacturer>> Get()
        {
            return await _ctx.Manufacturers.Include(m => m.Models).ToListAsync();
        }
    }
}
