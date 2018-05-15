using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesAndMagic.Areas.Admin.Models
{
    public class ShowTimeFilm
    {
        public int Id { get; set; }

        [Required]
        public DateTime ShowTime { get; set; }
    }
}