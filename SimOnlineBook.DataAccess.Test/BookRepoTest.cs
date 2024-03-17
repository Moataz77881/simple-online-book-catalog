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
        private Guid BookIdTest1 = Guid.NewGuid();
        private Guid BookIdTest2 = Guid.NewGuid();
        private Guid GenreIdTest1 = Guid.NewGuid();
        private Guid GenreIdTest2 = Guid.NewGuid();
        private Guid AutherIdTest1 = Guid.NewGuid();
        private Guid AutherIdTest2 = Guid.NewGuid();

        private DbContextOptions options;
        private Books book1;
        private Books book2;

        public BookRepoTest()
        {
            book1 = new Books()
            {
                Id = BookIdTest1,
                Name = "Moataz's Story",
                genresId = GenreIdTest1,
                authorId = AutherIdTest1,
            };
            book2 = new Books()
            {
                Id = BookIdTest2,
                Name = "Mohab's Story",
                genresId = GenreIdTest2,
                authorId = AutherIdTest2,
            };
        }
        [SetUp]
        public void sutSetUp() 
        {
            options = new DbContextOptionsBuilder<SimOnBookDbContext>()
                .UseInMemoryDatabase(databaseName: "BookRepo_Test").Options;
            
        }
        [Test]
        public async Task createNewBook_InputBooksObj_CheckTheValuesFromDatabase() 
        {
            //Arrange
            var moqObj = new Mock<ILogger<BookRepo>>();
            //Act
            using (var context = new SimOnBookDbContext(options)) 
            {
                var bookRepoObj = new BookRepo(context,moqObj.Object);
                await bookRepoObj.createNewBook(book1);

            }
            //Assert
            using (var context = new SimOnBookDbContext(options)) 
            {
                var bookRepository = context.Books.FirstOrDefault(x => x.Id == BookIdTest1);
                Assert.That(bookRepository.Name, Is.EqualTo("Moataz's Story"));
                Assert.That(bookRepository.genresId, Is.EqualTo(GenreIdTest1));
                Assert.That(bookRepository.authorId, Is.EqualTo(AutherIdTest1));
            }
        }
        [Test]
        public async Task getAllBooks_WithNoInputParameter_ShuldGiveListOfBooks() 
        {
            //Arrange
            var expectedResult = new List<Books> { book1, book2 }; 
            var MoqObj = new Mock<ILogger<BookRepo>>();
            using (var context = new SimOnBookDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new BookRepo(context, MoqObj.Object);
                await repository.createNewBook(book1);
                await repository.createNewBook(book2);
            }
            //Act
            List<Books> actualResult;
            using (var context = new SimOnBookDbContext(options)) 
            {
                var repository = new BookRepo(context, MoqObj.Object);
                actualResult = await repository.getAllBooks();
            }
            //Assert
            Assert.That(actualResult[0].Id, Is.EqualTo(expectedResult[0].Id));
            Assert.That(actualResult[0].Name, Is.EqualTo(expectedResult[0].Name));
            Assert.That(actualResult[0].genresId, Is.EqualTo(expectedResult[0].genresId));
            Assert.That(actualResult[0].authorId, Is.EqualTo(expectedResult[0].authorId));

            Assert.That(actualResult[1].Id, Is.EqualTo(expectedResult[1].Id));
            Assert.That(actualResult[1].Name, Is.EqualTo(expectedResult[1].Name));
            Assert.That(actualResult[1].genresId, Is.EqualTo(expectedResult[1].genresId));
            Assert.That(actualResult[1].authorId, Is.EqualTo(expectedResult[1].authorId));
        }
    }
}
