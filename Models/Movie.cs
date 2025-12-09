using System.IO;

namespace Linca_David_ProiectMPA.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int ReleaseYear { get; set; }
        public string Synopsis { get; set; }


        public int? GenreID { get; set; }
        public Genre? Genre { get; set; }


        public int? DirectorID { get; set; }
        public Director? Director { get; set; }


        public int? StudioID { get; set; }
        public Studio? Studio { get; set; }
    }
}
