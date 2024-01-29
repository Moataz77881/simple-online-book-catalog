using System.ComponentModel.DataAnnotations;

namespace simple_online_book_catalog.models.DTOModel.BookDTO
{
    public class CreateBookDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string? numberOfPages { get; set; }
        public string? imageOfBook { get; set; }

        [Required]
        public Guid genresId { get; set; }
        [Required]
        public Guid authorId { get; set; }
    }
}
