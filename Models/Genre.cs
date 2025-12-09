using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linca_David_ProiectMPA.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Movie>? Movies { get; set; }
    }
}
