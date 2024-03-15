using System.ComponentModel.DataAnnotations;

namespace simple_online_book_catalog.models.DTOModel.AuthorDTOs
{
    public class createAuthorDTO
    {
        [Required]
        public string Name { get; set; }
        public string? photoOfTheAuthor { get; set; }
    }
}
