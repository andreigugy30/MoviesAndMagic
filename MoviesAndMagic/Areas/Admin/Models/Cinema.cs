using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesAndMagic.Areas.Admin.Models
{
    public class Cinema
    {
        public int Id { get; set; }

        [Required]
        public string CinemaCity { get; set; }
    }
}