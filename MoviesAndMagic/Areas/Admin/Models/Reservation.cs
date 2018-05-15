using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesAndMagic.Areas.Admin.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required]
        public int ShowId { get; set; }
        [Required]
        public int SeatId { get; set; }

        public string ShowName { get; set; }

        public int SeatNo { get; set; }
    }
}