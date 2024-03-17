using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOnlineBook.DataAccess.Test
{
    public class GenresRepoTest
    {
        private Guid genreIdTest1 = Guid.NewGuid();
        private Guid genreIdTest2 = Guid.NewGuid();

        private Genres genre1;
        private Genres genre2;

        private DbContextOptions options;
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
        }
        [Test]
        public async Task CreateGenre_InputGenresOpject_CheckTheValuesFromDatabase() 
        {
            //Arrange
            
            var maqObj = new Mock<ILogger<GenresRepo>>();
            //Act
            using (var context = new SimOnBookDbContext(options)) 
            {
                var genreObj = new GenresRepo(context,maqObj.Object);
                await genreObj.CreateGenre(genre1);
            }
            //Assert
            using (var context = new SimOnBookDbContext(options)) 
            {
                var responseOpj = context.Genres.FirstOrDefault(x => x.Id == genreIdTest1);
                Assert.That(responseOpj, Is.Not.Null);
                Assert.That(responseOpj.genresOfTheBook, Is.EqualTo("Story"));
            }
        }
        [Test]
        public async Task getAllGenres_WithNoInputParameter_ShouldGiveListOfGenres() 
        {
            //Arrang
            var moqObj = new Mock<ILogger<GenresRepo>>();
            var ExpectedResult = new List<Genres> { genre1, genre2 };
            using (var context = new SimOnBookDbContext(options)) 
            {
                context.Database.EnsureDeleted();
                var repository = new GenresRepo(context, moqObj.Object);
                await repository.CreateGenre(genre1);
                await repository.CreateGenre(genre2);
            }
            //Act
            List<Genres> actualResult;
            using (var context = new SimOnBookDbContext(options)) 
            {
                var repository = new GenresRepo(context, moqObj.Object);
                actualResult = await repository.getAllGenresAsync();
            }
            //Assert
            Assert.That(actualResult[0].Id, Is.EqualTo(ExpectedResult[0].Id));
            Assert.That(actualResult[0].genresOfTheBook, Is.EqualTo(ExpectedResult[0].genresOfTheBook));
            Assert.That(actualResult[1].Id, Is.EqualTo(ExpectedResult[1].Id));
            Assert.That(actualResult[1].genresOfTheBook, Is.EqualTo(ExpectedResult[1].genresOfTheBook));

        }
    }
}
