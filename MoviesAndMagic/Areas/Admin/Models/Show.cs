using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAndMagic.Areas.Admin.Models
{
    public class Show
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public int FilmId { get; set; }
        public int ShowTimeFilmId { get; set; }

        public string CinemaCity { get; set; }
        public string FilmName { get; set; }
        public string ShowTimeFilm { get; set; }
    }
}