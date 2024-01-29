using simple_online_book_catalog.Models;

namespace simple_online_book_catalog.Repository.RepositoryInterfaces
{
    public interface IBook
    {
        public Task<Books> createNewBook(Books books);
        public Task<List<Books>> getAllBooks();
    }
}
