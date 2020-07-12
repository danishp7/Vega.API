using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Models
{
    public class Accessory
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Car Cover")]
        public bool IsCarCover { get; set; }

        [Required]
        [Display(Name = "Seat Covers")]
        public bool IsSeatCover { get; set; }

        [Required]
        [Display(Name = "Air Bags")]
        public bool IsAirBags { get; set; }

        [Required]
        [Display(Name = "Steering Lock")]
        public bool IsSteeringLock { get; set; }

        [Required]
        [Display(Name = "Steering Cover")]
        public bool IsSteeringCover { get; set; }

        [Required]
        [Display(Name = "Puncture Kit")]
        public bool IsPunctureKit { get; set; }

        // for accessory and vehicle relation
        public ICollection<VehicleAccessory> Vehicles { get; set; }
    }
}
