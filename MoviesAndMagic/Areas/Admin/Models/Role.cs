using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesAndMagic.Areas.Admin.Models
{
    public class Role
    {
        [Required]
        public int Id { get; set; }

        public string RoleName { get; set; }
    }
}