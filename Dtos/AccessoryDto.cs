using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.API.Models;

namespace Vega.API.Dtos
{
    public class AccessoryDto
    {
        public bool IsCarCover { get; set; }
        public bool IsSeatCover { get; set; }
        public bool IsAirBags { get; set; }
        public bool IsSteeringLock { get; set; }
        public bool IsSteeringCover { get; set; }
        public bool IsPunctureKit { get; set; }
    }
}
