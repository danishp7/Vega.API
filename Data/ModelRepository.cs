using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Data
{
    public class ModelRepository : IModelRepository
    {
        private readonly DataContext _ctx;
        public ModelRepository(DataContext context)
        {
            _ctx = context;
        }
        public async Task<IEnumerable<Model>> GetModels()
        {
            return await _ctx.Models.Include(v => v.Vehicles).ToListAsync();
        }

        public async Task<Model> GetModel(int id)
        {
            var model = await _ctx.Models.Include(v => v.Vehicles).SingleOrDefaultAsync(m => m.Id == id);
            if (model == null)
                return null;

            return model;
        }

    }
}
