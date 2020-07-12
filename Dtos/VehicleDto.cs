using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Dtos
{
    public class VehicleDto
    {
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Vehicle name must be at least 3 character long.")]
        public string Name { get; set; }

        [Required]
        public bool IsRegistered { get; set; }
        
        [Required]
        public double Price { get; set; }

        [Required]
        public int ModelId { get; set; }
        public ICollection<VehicleAccessory> Accessories { get; set; }
    }
}
