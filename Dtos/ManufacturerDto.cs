using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Dtos
{
    public class ManufacturerDto
    {
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Manufacturer name must be at least 3 character long.")]
        public string Name { get; set; }
        public ICollection<ModelDto> Models { get; set; }
    }
}
