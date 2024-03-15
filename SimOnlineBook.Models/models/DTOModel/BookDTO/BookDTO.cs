using simple_online_book_catalog.models.DTOModel.AuthorDTOs;
using simple_online_book_catalog.models.DTOModel.GenresDTO;
using simple_online_book_catalog.models.DTOModel.ImageDTO;

namespace simple_online_book_catalog.models.DTOModel.BookDTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? numberOfPages { get; set; }
        public string? imageOfBook { get; set; }
        public GetGenresDTO Genres { get; set; }
        public getAuthorDTO Authors { get; set; }
        public  responseImageDTO Image { get; set; }
    }
}
