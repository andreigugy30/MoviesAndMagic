using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesAndMagic.Areas.Admin.Models
{
    public class Film
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a film name!")]
        [MinLength(2, ErrorMessage = "Name field must have at least two characters!")]
        public string Name { get; set; }

        //public string Genre { get; set; }

        [Required(ErrorMessage = "You must enter a film description!")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string GenreName { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
    }
}