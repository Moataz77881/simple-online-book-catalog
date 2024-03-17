using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using simple_online_book_catalog.IServices.Services;
using simple_online_book_catalog.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOnlineBook.Core.Test
{
    [TestFixture]
    public class AuthorRepoServiceTest
    {
        private Mock<IMapper> _mockObjMapper;
        private Mock<ILogger<AuthorServiceRepo>> _mockObjLog;
        private Mock<IAuthor> _mockObjAuthor;
        private AuthorServiceRepo _serviceRepo;

        [SetUp]
        public void Setup() 
        {
            _mockObjAuthor = new Mock<IAuthor>();
            _mockObjLog = new Mock<ILogger<AuthorServiceRepo>>();
            _mockObjMapper = new Mock<IMapper>();
            _serviceRepo = new AuthorServiceRepo(
                _mockObjAuthor.Object,
                _mockObjMapper.Object,
                _mockObjLog.Object
                );

        }
        [Test]
        public async Task getAllAuthorService_WithNoInputParameter_ShouldCallGetAllAuthor() 
        {
            await _serviceRepo.getAllAuthorService();
            _mockObjAuthor.Verify(x => x.getAllAuthors(), Times.Once);
        }
    }
}
