using Microsoft.EntityFrameworkCore;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

namespace simple_online_book_catalog.Repository
{
    public class BookRepo : IBook
    {
        private readonly SimOnBookDbContext dbContext;

        public BookRepo(SimOnBookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Books> createNewBook(Books books)
        {
            await dbContext.Books.AddAsync(books);
            await dbContext.SaveChangesAsync();
            return books;
        }

        public async Task<List<Books>> getAllBooks()
        {
             var books = dbContext.Books.Include("Genres").Include("Authors");
            return await books.ToListAsync();
        }
    }
}
