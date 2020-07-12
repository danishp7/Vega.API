using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Data
{
    public class VegaSharedRepository : IVegaSharedRepository
    {
        private readonly DataContext _ctx;
        public VegaSharedRepository(DataContext context)
        {
            _ctx = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _ctx.Add(entity);
        }
        public async Task<bool> SaveAll()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }
        public void Delete<T>(T entity) where T : class
        {
            _ctx.Remove(entity);
        }
    }
}
