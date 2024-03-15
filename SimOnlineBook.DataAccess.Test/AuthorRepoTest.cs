using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository;

namespace SimOnlineBook.DataAccess.Test
{
    [TestFixture]
    public class AuthorRepoTest
    {
        private Authors authorsTestObj1;
        private Authors authorsTestObj2;
        private Guid AutherId = Guid.NewGuid();
        public AuthorRepoTest()
        {
            authorsTestObj1 = new Authors() 
            {
                Id = AutherId,
                Name = "moataz",
                photoOfTheAuthor="",
            };
            authorsTestObj2 = new Authors()
            {
                Id = AutherId,
                Name = "moataz",
                photoOfTheAuthor = "",
            };
        }

        [Test]
        public void createAuthor_ObjectAutherInput_CheckTheValuesFromDatabase() 
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SimOnBookDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Reposetory").Options;
            var moqObj = new Mock<ILogger<AuthorRepo>>();
            //Act
            using (var conext = new SimOnBookDbContext(options)) 
            {
                var repository = new AuthorRepo(conext,moqObj.Object);
                repository.createAuthor(authorsTestObj1);
            }

            //Assert
            using (var context = new SimOnBookDbContext(options)) 
            {
                var AutherFromDb = context.Authors.FirstOrDefault(x => x.Id == AutherId);
                Assert.That(AutherFromDb, Is.Not.Null);
                Assert.That(AutherFromDb.Name, Is.EqualTo(authorsTestObj1.Name));
                Assert.That(AutherFromDb.photoOfTheAuthor, Is.EqualTo(authorsTestObj1.photoOfTheAuthor));

            }
        }

        [Test]
        public void getAllAuthers_WithNoInputPrameter_ShouldGiveListOfAuthers() 
        {
            //Arrange
            var expectedResult = new List<Authors> {authorsTestObj1,authorsTestObj2}; 
            var options = new DbContextOptionsBuilder<SimOnBookDbContext>()
                .UseInMemoryDatabase(databaseName: "TestRepository").Options;
            var moqOpj = new Mock<ILogger<AuthorRepo>>();
            using (var context = new SimOnBookDbContext(options)) 
            {
                var repository = new AuthorRepo(context, moqOpj.Object);
                repository.createAuthor(authorsTestObj1);
                repository.createAuthor(authorsTestObj2);
            }
            //Act
            Task<List<Authors>> ActualList;
            using (var context = new SimOnBookDbContext(options)) 
            {
                var repository = new AuthorRepo(context, moqOpj.Object);
                ActualList = repository.getAllAuthors();
                //ActualList = context.Authors.ToList();
            }
            //Assert
            Assert.That(expectedResult,Is.EqualTo(ActualList));
        }
    }
}
