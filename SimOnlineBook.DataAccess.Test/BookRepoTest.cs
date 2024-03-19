using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.models.DomainModel;
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
        private Mock<ILogger<BookRepo>> moqLoggerObj;
        private SimOnBookDbContext context;
        private BookRepo bookRepoObj;

        private Books book1;
        private Books book2;
        //private Genres genres1;
        //private Genres genres2;
        //private Authors Authors1;
        //private Authors Authors2;

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
            //genres1 = new Genres()
            //{
            //    Id = GenreIdTest1,
            //    genresOfTheBook = "story1"
            //};
            //genres2 = new Genres()
            //{
            //    Id = GenreIdTest2,
            //    genresOfTheBook = "story2"
            //};
            //Authors1 = new Authors()
            //{
            //    Id = AutherIdTest1,
            //    Name = "moataz",
            //};
            //Authors2 = new Authors()
            //{
            //    Id = AutherIdTest2,
            //    Name = "mohab",
            //};

        }
        [SetUp]
        public void sutSetUp()
        {
            options = new DbContextOptionsBuilder<SimOnBookDbContext>()
                .UseInMemoryDatabase(databaseName: "BookRepo_Test").Options;
            moqLoggerObj = new Mock<ILogger<BookRepo>>();
            context = new SimOnBookDbContext(options);
            bookRepoObj = new BookRepo(context, moqLoggerObj.Object);

        }
        [Test]
        public async Task createNewBook_InputBooksObj_CheckTheValuesFromDatabase()
        {
            //Arrange
            //Act
            await bookRepoObj.createNewBook(book1);

            //Assert
            var bookRepository = context.Books.FirstOrDefault(x => x.Id == BookIdTest1);
            Assert.That(bookRepository.Name, Is.EqualTo("Moataz's Story"));
            Assert.That(bookRepository.genresId, Is.EqualTo(GenreIdTest1));
            Assert.That(bookRepository.authorId, Is.EqualTo(AutherIdTest1));
        }
        [Test]
        public async Task getAllBooks_WithNoInputParameter_ShuldGiveListOfBooks()
        {
            //Arrange
            var expectedResult = new List<Books> { book1, book2 };

            //MoqIHttpContextAccessorobj = new Mock<IHttpContextAccessor>();
            //MoqIWebHostEnvironmentObj = new Mock<IWebHostEnvironment>();
            //MoqLoggerImageObj = new Mock<ILogger<ImageRepo>>();

            context.Database.EnsureDeleted();
            //var repositoryImageRepo = new ImageRepo
            //    (
            //        MoqIWebHostEnvironmentObj.Object
            //        , MoqIHttpContextAccessorobj.Object
            //        , context
            //        , MoqLoggerImageObj.Object
            //    );
            //await repositoryImageRepo.uploadImage(Image1);
            //await repositoryImageRepo.uploadImage(Image2);

            await bookRepoObj.createNewBook(book1);
            await bookRepoObj.createNewBook(book2);

            //Act
            List<Books> actualResult;



            actualResult = await bookRepoObj.getAllBooks();

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
        [Test]
        public async Task deleteBook_GuidInputParameter_ShouldReturnBookDeleted()
        {
            //Arrange
            Books? responseResult;
            context.Database.EnsureDeleted();
            await bookRepoObj.createNewBook(book1);
            
            //Act
            responseResult = await bookRepoObj.deleteBook(BookIdTest1);

            //Assert
            Assert.That(responseResult, Is.Not.Null);
            Assert.That(responseResult.Id, Is.EqualTo(BookIdTest1));
        }
        [Test]
        public async Task deleteBook_GuidInputParameter_ShouldReturnNull()
        {
            //Arrange
            Books? responseResult;
            context.Database.EnsureDeleted();
            await bookRepoObj.createNewBook(book1);
            
            //Act
            responseResult = await bookRepoObj.deleteBook(BookIdTest2);

            //Assert
            Assert.That(responseResult, Is.Null);
        }
        [Test]
        public async Task updateBook_TakesTwoParameterBookObjAndGuid_ShouldReturnObjectUpdated()
        {
            //Arrange
            Books? book;
            context.Database.EnsureDeleted();
            await bookRepoObj.createNewBook(book1);
            
            //Act
            book = await bookRepoObj.updateBook(book2, BookIdTest1);
            
            //Assert
            Assert.That(book, Is.Not.Null);
            Assert.That(book.Name, Is.EqualTo(book2.Name));
            Assert.That(book.genresId, Is.EqualTo(book2.genresId));
            Assert.That(book.authorId, Is.EqualTo(book2.authorId));

        }
        [Test]
        public async Task updateBook_TakesTwoParameterBookObjAndGuid_ShouldReturnNull()
        {
            //Arrange
            Books? book;
            context.Database.EnsureDeleted();
            await bookRepoObj.createNewBook(book1);
            
            //Act
            book = await bookRepoObj.updateBook(book2, Guid.NewGuid());
            
            //Assert
            Assert.That(book, Is.Null);
        }
    }
}
