using System.ComponentModel.DataAnnotations;

namespace simple_online_book_catalog.models.DTOModel.ImageDTO
{
    public class ImagesDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string fileName { get; set; }
        public string? fileDescription { get; set; }
    }
}
