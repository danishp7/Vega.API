using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Data
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Manufacturer>> Get();
    }
}
