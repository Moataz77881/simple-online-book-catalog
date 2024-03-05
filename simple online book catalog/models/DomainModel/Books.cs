
using simple_online_book_catalog.models.DomainModel;

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

        public Guid imageId { get; set; }

        public Genres Genres { get; set; }
        public Authors Authors { get; set; }
        public Image Image { get; set; }
    }
}
