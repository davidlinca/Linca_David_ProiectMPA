namespace Linca_David_ProiectMPA.Models
{
    public class Studio
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Movie>? Movies { get; set; }
    }
}
