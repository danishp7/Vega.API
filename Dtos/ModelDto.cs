using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Dtos
{
    public class ModelDto
    {
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Model name must be at least 3 character long.")]
        public string Name { get; set; }
        public ICollection<VehicleDto> Vehicles { get; set; }
    }
}
