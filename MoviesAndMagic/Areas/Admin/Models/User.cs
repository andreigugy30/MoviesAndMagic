using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesAndMagic.Areas.Admin.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Firstname field must have at least two characters!")]
        public string Firstname { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Lastname field must have at least three characters!")]
        public string Lastname { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Username field must have at least four characters!")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string RoleName { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        public virtual Role Role { get; set; }
    }
}