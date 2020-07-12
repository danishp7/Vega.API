using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Vehicle Name")]
        [StringLength(11, ErrorMessage = "Vehicle Name must be in between 3 or 11 characters long!", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Registered")]
        public bool IsRegistered { get; set; }

        [Required]
        [Display(Name = "Vehicle Price")]
        public double Price { get; set; }

        // relation with vehicle Model
        public Model Model { get; set; }
        public int ModelId { get; set; }

        // relation with accessory
        public ICollection<VehicleAccessory> Accessories { get; set; }

        // relation with user
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
