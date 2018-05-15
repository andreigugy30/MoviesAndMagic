using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesAndMagic.Areas.Admin.Models
{
    public class Seat
    {
        public int Id { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive seat number")]
        public int SeatNo { get; set; }

        public bool Status { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}