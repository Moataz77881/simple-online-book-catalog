using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

namespace simple_online_book_catalog.Repository
{
    public class BookRepo : IBook
    {
        private readonly SimOnBookDbContext dbContext;
        private readonly ILogger<BookRepo> logger;

        public BookRepo(SimOnBookDbContext dbContext, ILogger<BookRepo> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<Books> createNewBook(Books books)
        {
            logger.LogInformation("you are in createNewBook repository");
            await dbContext.Books.AddAsync(books);
            await dbContext.SaveChangesAsync();
            return books;
        }

        public async Task<Books?> deleteBook(Guid id)
        {
            logger.LogInformation("you are in deleteBook method with parameter  repository");

            var Book = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (Book != null)
            {
                var Result = dbContext.Books.Remove(Book);
                await dbContext.SaveChangesAsync();
                return Result.Entity;
            }
            return null;
        }

        public async Task<List<Books>> getAllBooks()
        {
            logger.LogInformation("you are in getAllBooks repository");

            var books = await dbContext.Books
                .Include(x=> x.Genres)
                .Include(x=>x.Authors)
                .Include(x=>x.Image)
                .ToListAsync();
            return books;
        }

        public async Task<Books?> updateBook(Books book, Guid id)
        {
            logger.LogInformation("you are in updateBook repository");

            var bookExist = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if(bookExist == null) { return null; }
            bookExist.Name = book.Name;
            bookExist.numberOfPages = book.numberOfPages;
            bookExist.imageOfBook = book.imageOfBook;
            bookExist.genresId = book.genresId;
            bookExist.authorId = book.authorId;
            await dbContext.SaveChangesAsync();
            return bookExist;
        }
    }
}
