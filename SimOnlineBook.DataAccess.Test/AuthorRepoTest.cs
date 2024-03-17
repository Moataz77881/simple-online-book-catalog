using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository;
using System.Collections;

namespace SimOnlineBook.DataAccess.Test
{
    [TestFixture]
    public class AuthorRepoTest
    {
        private Authors authorsTestObj1;
        private Authors authorsTestObj2;
        private Guid AutherIdObj1 = Guid.NewGuid();
        private Guid AutherIdObj2 = Guid.NewGuid();

        private DbContextOptions<SimOnBookDbContext> options;
        public AuthorRepoTest()
        {
            authorsTestObj1 = new Authors() 
            {
                Id = AutherIdObj1,
                Name = "moataz",
                photoOfTheAuthor="",
            };
            authorsTestObj2 = new Authors()
            {
                Id = AutherIdObj2,
                Name = "mohab",
                photoOfTheAuthor = "",
            };
        }

        [SetUp]
        public void SetUp() 
        {
            options = new DbContextOptionsBuilder<SimOnBookDbContext>()
               .UseInMemoryDatabase(databaseName: "Test_Reposetory").Options;
        }

        [Test]
        public async Task createAuthor_ObjectAutherInput_CheckTheValuesFromDatabase() 
        {
            //Arrange
           
            var moqObj = new Mock<ILogger<AuthorRepo>>();
            //Act
            using (var conext = new SimOnBookDbContext(options)) 
            {
                var repository = new AuthorRepo(conext,moqObj.Object);
                await repository.createAuthor(authorsTestObj1);
            }

            //Assert
            using (var context = new SimOnBookDbContext(options)) 
            {
                var AutherFromDb = context.Authors.FirstOrDefault(x => x.Id == AutherIdObj1);
                Assert.That(AutherFromDb, Is.Not.Null);
                Assert.That(AutherFromDb.Name, Is.EqualTo(authorsTestObj1.Name));
                Assert.That(AutherFromDb.photoOfTheAuthor, Is.EqualTo(authorsTestObj1.photoOfTheAuthor));

            }
        }

        [Test]
        public async Task getAllAuthers_WithNoInputPrameter_ShouldGiveListOfAuthers() 
        {
            //Arrange
            var expectedResult = new List<Authors> {authorsTestObj1,authorsTestObj2}; 
            var moqOpj = new Mock<ILogger<AuthorRepo>>();
            using (var context = new SimOnBookDbContext(options)) 
            {
                var repository = new AuthorRepo(context, moqOpj.Object);
                context.Database.EnsureDeleted();
                await repository.createAuthor(authorsTestObj1);
                await repository.createAuthor(authorsTestObj2);
            }
            //Act
            List<Authors> ActualList;
            using (var context = new SimOnBookDbContext(options)) 
            {
                var repository = new AuthorRepo(context, moqOpj.Object);
                ActualList = await repository.getAllAuthors();
                //ActualList = context.Authors.ToList();
            }
            //Assert
            Assert.That(expectedResult[0].Id, Is.EqualTo(ActualList[0].Id));
            Assert.That(expectedResult[0].Name, Is.EqualTo(ActualList[0].Name));
            Assert.That(expectedResult[1].Id, Is.EqualTo(ActualList[1].Id));
            Assert.That(expectedResult[1].Name, Is.EqualTo(ActualList[1].Name));

            //CollectionAssert.AreEqual(expectedResult, ActualList,new AuthorsCompare());
        }
    }
    //public class AuthorsCompare : IComparer {
    //    public int Compare(object? x, object? y)
    //    {
    //        var AuthorList1 = (Authors)x;
    //        var AuthorList2 = (Authors)y;

    //        if (AuthorList1.Id != AuthorList2.Id) 
    //        {
    //            return 1;
    //        }
    //        return 0;

    //    }
    //}
}
