using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository;


namespace SimOnlineBook.DataAccess.Test
{
    public class GenresRepoTest
    {
        private Guid genreIdTest1 = Guid.NewGuid();
        private Guid genreIdTest2 = Guid.NewGuid();
        private Mock<ILogger<GenresRepo>> moqLoggerObj;

        private Genres genre1;
        private Genres genre2;

        private DbContextOptions options;
        private SimOnBookDbContext context;
        private GenresRepo repositoryObj;

        public GenresRepoTest()
        {

            genre1 = new Genres()
            {
                Id = genreIdTest1,
                genresOfTheBook = "Story"
            };
            genre2 = new Genres()
            {
                Id = genreIdTest2,
                genresOfTheBook = "mmmmm"
            };
        }
        [SetUp]
        public void SetUp() 
        {
            options = new DbContextOptionsBuilder<SimOnBookDbContext>()
                .UseInMemoryDatabase(databaseName: "Genres_Test").Options;
            moqLoggerObj = new Mock<ILogger<GenresRepo>>();
            context = new SimOnBookDbContext(options);
            repositoryObj = new GenresRepo(context, moqLoggerObj.Object);

        }
        [Test]
        public async Task CreateGenre_InputGenresOpject_CheckTheValuesFromDatabase() 
        {
            //Arrange
            //Act
                await repositoryObj.CreateGenre(genre1);
            //Assert
            
                var responseOpj = context.Genres.FirstOrDefault(x => x.Id == genreIdTest1);
                Assert.That(responseOpj, Is.Not.Null);
                Assert.That(responseOpj.genresOfTheBook, Is.EqualTo("Story"));
            
        }
        [Test]
        public async Task getAllGenres_WithNoInputParameter_ShouldGiveListOfGenres() 
        {
            //Arrang
            var ExpectedResult = new List<Genres> { genre1, genre2 };
            
            context.Database.EnsureDeleted();
            await repositoryObj.CreateGenre(genre1);
            await repositoryObj.CreateGenre(genre2);
            
            //Act
            List<Genres> actualResult;
            
            actualResult = await repositoryObj.getAllGenresAsync();
            
            //Assert
            Assert.That(actualResult[0].Id, Is.EqualTo(ExpectedResult[0].Id));
            Assert.That(actualResult[0].genresOfTheBook, Is.EqualTo(ExpectedResult[0].genresOfTheBook));
            Assert.That(actualResult[1].Id, Is.EqualTo(ExpectedResult[1].Id));
            Assert.That(actualResult[1].genresOfTheBook, Is.EqualTo(ExpectedResult[1].genresOfTheBook));

        }
        [Test]
        public async Task deleteGenre_HasGuidParameter_RetrunsObjDeleted() 
        {
            //Arrange
                context.Database.EnsureDeleted();
                await repositoryObj.CreateGenre(genre1);
            //Act
            Genres? responseResult;
                responseResult = await repositoryObj.deleteGenre(genreIdTest1);
            //Assert
            Assert.That(responseResult, Is.Not.Null);
        }
        [Test]
        public async Task deleteGenre_HasGuidParameter_RetrunsNull()
        {
            //Arrange
            
                
                context.Database.EnsureDeleted();
                await repositoryObj.CreateGenre(genre1);
            //Act
            Genres? responseResult;
            
                context.Database.EnsureDeleted();
                responseResult = await repositoryObj.deleteGenre(Guid.NewGuid());
            
            //Assert
            Assert.That(responseResult, Is.Null);
        }
        [Test]
        public async Task updateGenre_TakesTwoParametersGuidAndGenresObj_ReturnsObjectUpdated() 
        {
            //Arrange
            
            context.Database.EnsureDeleted();
            await repositoryObj.CreateGenre(genre1);

            //Act
            Genres? responseResult;
            responseResult = await repositoryObj.updateGenre(genreIdTest1, genre2);

            //Assert
            Assert.That(responseResult, Is.Not.Null);
            Assert.That(responseResult.genresOfTheBook, Is.EqualTo(genre2.genresOfTheBook));
        }
        [Test]
        public async Task updateGenre_TakesTwoParametersGuidAndGenresObj_ReturnsNull()
        {
            //Arrange

            context.Database.EnsureDeleted();
            await repositoryObj.CreateGenre(genre1);

            //Act
            Genres? responseResult;
            responseResult = await repositoryObj.updateGenre(Guid.NewGuid(), genre2);

            //Assert
            Assert.That(responseResult, Is.Null);
        }
    }
}
