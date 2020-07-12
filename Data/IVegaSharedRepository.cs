using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Data
{
    public interface IVegaSharedRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveAll();
        void Delete<T>(T entity) where T : class;
    }
}
