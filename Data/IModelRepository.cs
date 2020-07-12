using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Data
{
    public interface IModelRepository
    {
        Task<IEnumerable<Model>> GetModels();
        Task<Model> GetModel(int id);
    }
}
