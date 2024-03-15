using simple_online_book_catalog.models.DTOModel.BookDTO;
using simple_online_book_catalog.Models;

namespace simple_online_book_catalog.Services.IServices
{
    public interface IBookService
    {
        public Task<BookDTO> createBookService(CreateBookDTO bookDTO);
        public Task<List<BookDTO>> getAllBooksService();
        public Task<BookDTO> updatebookService(CreateBookDTO bookDTO ,Guid id);
        public Task<string?> deleteBook(Guid id);
    }
}
