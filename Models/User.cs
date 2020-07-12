using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ContactNumber { get; set; }
        
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // relation with vehicles
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
