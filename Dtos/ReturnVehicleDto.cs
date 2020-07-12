using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Dtos
{
    public class ReturnVehicleDto
    {
        public string Name { get; set; }
        public bool IsRegistered { get; set; }
        public double Price { get; set; }
        public int ModelId { get; set; }
        public AccessoryDto Accessory { get; set; }
    }
}
