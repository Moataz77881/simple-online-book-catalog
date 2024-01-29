namespace simple_online_book_catalog.models.DTOModel.BookDTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? numberOfPages { get; set; }
        public string? imageOfBook { get; set; }

        public Guid genresId { get; set; }
        public Guid authorId { get; set; }
    }
}
