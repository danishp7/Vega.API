using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Dtos
{
    public class UserDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public int ContactNumber { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Email Id is required")]
        [Display(Name = "Email Id")]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Password must have at least 5 characters")]
        public string Password { get; set; }
    }
}
