using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Dtos
{
    public class ManufacturerDto
    {
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
