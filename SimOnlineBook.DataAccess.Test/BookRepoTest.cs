using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository;


namespace SimOnlineBook.DataAccess.Test
{
    [TestFixture]
    public class BookRepoTest
    {
        private Guid BookIdTest = Guid.NewGuid();
        private Guid GenreIdTest = Guid.NewGuid();
        private Guid AutherIdTest = Guid.NewGuid();
        private Books book;
        public BookRepoTest()
        {
            book = new Books()
            {
                Id = BookIdTest,
                Name = "Moataz's Story",
                genresId = GenreIdTest,
                authorId = AutherIdTest,
            };
        }
        [Test]
        public void createNewBook_InputBooksObj_CheckTheValuesFromDatabase() 
        {
            //Arrange
            var optios = new DbContextOptionsBuilder<SimOnBookDbContext>()
                .UseInMemoryDatabase(databaseName: "BookRepo_Test").Options;
            var moqObj = new Mock<ILogger<BookRepo>>();
            //Act
            using (var context = new SimOnBookDbContext(optios)) 
            {
                var bookRepoObj = new BookRepo(context,moqObj.Object);
                bookRepoObj.createNewBook(book);
            }
            //Assert
            using (var context = new SimOnBookDbContext(optios)) 
            {
                var bookRepository = context.Books.FirstOrDefault(x => x.Id == BookIdTest);
                Assert.That(bookRepository.Name, Is.EqualTo("Moataz's Story"));
                Assert.That(bookRepository.genresId, Is.EqualTo(GenreIdTest));
                Assert.That(bookRepository.authorId, Is.EqualTo(AutherIdTest));
            }
        }
    }
}
