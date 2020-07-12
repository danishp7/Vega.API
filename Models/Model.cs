using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Models
{
    public class Model
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Vehicle Model")]
        [StringLength(11, ErrorMessage = "Vehicle Model Name must be in between 3 or 11 characters long!", MinimumLength = 3)]
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }

        // for vehicle
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
