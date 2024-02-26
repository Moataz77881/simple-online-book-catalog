using AutoMapper;
using simple_online_book_catalog.models.DTOModel.BookDTO;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository;
using simple_online_book_catalog.Repository.RepositoryInterfaces;
using simple_online_book_catalog.Services.IServices;

namespace simple_online_book_catalog.Services.ServiceRepo
{
    public class BookServiceRepo : IBookService
    {
        private readonly IMapper mapper;
        private readonly IBook book;

        public BookServiceRepo(IMapper mapper, IBook book)
        {
            this.mapper = mapper;
            this.book = book;
        }
        public async Task<BookDTO> createBookService(CreateBookDTO bookDTO)
        {
            var domain = await book.createNewBook(mapper.Map<Books>(bookDTO));
            return mapper.Map<BookDTO>(domain);
        }

        public async Task<string?> deleteBook(Guid id)
        {
             var bookDeleted = await book.deleteBook(id);
            if (bookDeleted == null) return null;
            return "Book Deleted";

        }

        public async Task<List<BookDTO>> getAllBooksService()
        {
            var recivedBooks = await book.getAllBooks();
            return mapper.Map<List<BookDTO>>(recivedBooks);
        }

        public async Task<BookDTO> updatebookService(CreateBookDTO bookDTO, Guid id)
        {
            var bookUpdated = await book.updateBook(mapper.Map<Books>(bookDTO), id);
            return mapper.Map<BookDTO>(bookUpdated);
        }
    }
}
