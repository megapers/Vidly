using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(): base(){}

        [Required]
        [MaxLength(255)]
        public string DrivingLicense { get; set; }

        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }

    }
}
