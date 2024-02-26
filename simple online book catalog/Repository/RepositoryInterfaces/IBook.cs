using Microsoft.AspNetCore.Mvc;
using simple_online_book_catalog.Models;

namespace simple_online_book_catalog.Repository.RepositoryInterfaces
{
    public interface IBook
    {
        public Task<Books> createNewBook(Books books);
        public Task<List<Books>> getAllBooks();
        public Task<Books> updateBook(Books book, Guid id);
        public Task<Books?> deleteBook(Guid id);
    }
}
