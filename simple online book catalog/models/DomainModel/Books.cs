using System.ComponentModel.DataAnnotations.Schema;

namespace simple_online_book_catalog.Models
{
    public class Books
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? numberOfPages { get; set; }
        public string? imageOfBook { get; set; }

//        [ForeignKey("Genres")]
        public Guid genresId { get; set; } //one genres many book

 //       [ForeignKey("Authors")]
        public Guid authorId { get; set; } //one author many book
        
        public Genres Genres { get; set; }
        public Authors Authors { get; set; }
    }
}
