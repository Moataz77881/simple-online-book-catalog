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
        private Guid genreIdTest = Guid.NewGuid();
        private Genres genre;
        public GenresRepoTest()
        {
            genre = new Genres()
            {
                Id = genreIdTest,
                genresOfTheBook = "Story"
            };
        }
        [Test]
        public void CreateGenre_InputGenresOpject_CheckTheValuesFromDatabase() 
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SimOnBookDbContext>()
                .UseInMemoryDatabase(databaseName: "Genres_Test").Options;
            var maqObj = new Mock<ILogger<GenresRepo>>();
            //Act
            using (var context = new SimOnBookDbContext(options)) 
            {
                var genreObj = new GenresRepo(context,maqObj.Object);
                genreObj.CreateGenre(genre);
            }
            //Assert
            using (var context = new SimOnBookDbContext(options)) 
            {
                var responseOpj = context.Genres.FirstOrDefault(x => x.Id == genreIdTest);
                Assert.That(responseOpj, Is.Not.Null);
                Assert.That(responseOpj.genresOfTheBook, Is.EqualTo("Story"));
            }
        }
    }
}
