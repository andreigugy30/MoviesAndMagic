using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAndMagic.Areas.Admin.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; internal set; }
        public string Text { get; internal set; }
    }
}