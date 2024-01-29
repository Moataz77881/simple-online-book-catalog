using System.ComponentModel.DataAnnotations;

namespace simple_online_book_catalog.models.DTOModel.GenresDTO
{
    public class CreateGenre
    {
        [Required]
        public string genresOfTheBook { get; set; }
    }
}
