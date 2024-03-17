using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using simple_online_book_catalog.Repository.RepositoryInterfaces;
using simple_online_book_catalog.Services.ServiceRepo;


namespace SimOnlineBook.Core.Test
{
    [TestFixture]
    public class GenresServiceRepoTest
    {
        private Mock<IGenres> _mockObjGenres;
        private Mock<ILogger<GenresServiceRepo>> _mockObjLog;
        private Mock<IMapper> _mockObjMapper;
        private GenresServiceRepo GenresServiceRepo;

        [SetUp]
        public void Setup() 
        {
            _mockObjGenres = new Mock<IGenres>();
            _mockObjLog = new Mock<ILogger<GenresServiceRepo>>();
            _mockObjMapper = new Mock<IMapper>();
            GenresServiceRepo = new GenresServiceRepo(
                _mockObjMapper.Object,
                _mockObjGenres.Object,
                _mockObjLog.Object
                );
        }
        [Test]
        public async Task getAllGenres_WithNoParameter_ShouldCallgetAllGenresAsync() 
        {
            await GenresServiceRepo.getAllGenres();
            _mockObjGenres.Verify(x => x.getAllGenresAsync(), Times.Once);
        }
    }
}
