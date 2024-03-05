using System.ComponentModel.DataAnnotations;

namespace simple_online_book_catalog.Models
{
    public class Genres
    {
       // [Key]
        public Guid Id { get; set; }
        public string genresOfTheBook { get; set; }
        public List<Books> Books { get; set; }

    }
}
