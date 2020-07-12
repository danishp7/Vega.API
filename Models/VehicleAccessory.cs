using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Models
{
    public class VehicleAccessory
    {
        // we do not create id column for this
        // we'll make composite key using vehicleid and accessorid 
        // so that 1 accessory can be added in 1 vehicle only 1 time
        // for vehicle and accessory relation
        public int VehicleId { get; set; }
        public int AccessoryId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Accessory Accessory { get; set; }
    }
}
