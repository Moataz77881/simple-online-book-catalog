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
    public class AuthorRepoTest
    {
        private Authors authorsTestObj1;
        private Authors authorsTestObj2;
        private Guid AutherIdObj1 = Guid.NewGuid();
        private Guid AutherIdObj2 = Guid.NewGuid();

        private Mock<ILogger<AuthorRepo>> moqObj;
        private DbContextOptions<SimOnBookDbContext> options;
        private SimOnBookDbContext context;
        private AuthorRepo repositoryObj;

        public AuthorRepoTest()
        {
            authorsTestObj1 = new Authors()
            {
                Id = AutherIdObj1,
                Name = "moataz",
                photoOfTheAuthor = "",
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
            moqObj = new Mock<ILogger<AuthorRepo>> ();
            context = new SimOnBookDbContext(options);
            repositoryObj = new AuthorRepo(context, moqObj.Object);
        }

        [Test]
        public async Task createAuthor_ObjectAutherInput_CheckTheValuesFromDatabase()
        {
            //Arrange
            //Act
            await repositoryObj.createAuthor(authorsTestObj1);

            //Assert

            var AutherFromDb = context.Authors.FirstOrDefault(x => x.Id == AutherIdObj1);
            Assert.That(AutherFromDb, Is.Not.Null);
            Assert.That(AutherFromDb.Name, Is.EqualTo(authorsTestObj1.Name));
            Assert.That(AutherFromDb.photoOfTheAuthor, Is.EqualTo(authorsTestObj1.photoOfTheAuthor));
        }

        [Test]
        public async Task getAllAuthers_WithNoInputPrameter_ShouldGiveListOfAuthers()
        {
            //Arrange
            var expectedResult = new List<Authors> { authorsTestObj1, authorsTestObj2 };

            context.Database.EnsureDeleted();
            await repositoryObj.createAuthor(authorsTestObj1);
            await repositoryObj.createAuthor(authorsTestObj2);

            //Act
            List<Authors> ActualList;

            ActualList = await repositoryObj.getAllAuthors();

            //Assert
            Assert.That(expectedResult[0].Id, Is.EqualTo(ActualList[0].Id));
            Assert.That(expectedResult[0].Name, Is.EqualTo(ActualList[0].Name));
            Assert.That(expectedResult[1].Id, Is.EqualTo(ActualList[1].Id));
            Assert.That(expectedResult[1].Name, Is.EqualTo(ActualList[1].Name));

            //CollectionAssert.AreEqual(expectedResult, ActualList,new AuthorsCompare());
        }

        [Test]
        public async Task removeAuthor_WhenItHasValue_ShouldRemovedAuthorObj()
        {
            //Arrange
            Authors? responseResult;

            context.Database.EnsureDeleted();
            await repositoryObj.createAuthor(authorsTestObj1);

            //Act
            responseResult = await repositoryObj.removeAuther(AutherIdObj1);

            //Assert
            Assert.That(responseResult, Is.Not.Null);
            Assert.That(authorsTestObj1.Id, Is.EqualTo(responseResult?.Id));
        }
        [Test]
        public async Task removeAuthor_WhenItHasValue_ReturnsNull()
        {
            //Arrange
            Authors? responseResult;

            context.Database.EnsureDeleted();
            await repositoryObj.createAuthor(authorsTestObj1);
            
            //Act
            responseResult = await repositoryObj.removeAuther(Guid.NewGuid());
            
            //Assert
            Assert.That(responseResult, Is.Null);
        }
        [Test]
        public async Task updateAuthor_TakesGuidAndTwoParametersAuthorObj_ShouldReturnObjUpdated()
        {
            //Arrange
            Authors? responseResult;

            context.Database.EnsureDeleted();
            await repositoryObj.createAuthor(authorsTestObj1);

            //Act 
            responseResult = await repositoryObj.updateAuthor(AutherIdObj1, authorsTestObj2);

            //Assert 
            Assert.That(responseResult, Is.Not.Null);
            Assert.That(responseResult.Name, Is.EqualTo(authorsTestObj2.Name));
        }
        [Test]
        public async Task updateAuthor_TakesGuidAndTwoParametersAuthorObj_ShouldReturnNull()
        {
            //Arrange
            Authors? responseResult;
            context.Database.EnsureDeleted();
            await repositoryObj.createAuthor(authorsTestObj1);

            //Act 
            responseResult = await repositoryObj.updateAuthor(Guid.NewGuid(), authorsTestObj2);
            
            //Assert 
            Assert.That(responseResult, Is.Null);
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
