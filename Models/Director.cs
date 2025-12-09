using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linca_David_ProiectMPA.Models
{
    public class Director
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }

        public ICollection<Movie>? Movies { get; set; }
    }
}
