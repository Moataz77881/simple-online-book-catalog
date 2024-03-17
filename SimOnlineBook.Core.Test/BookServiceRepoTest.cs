using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using simple_online_book_catalog.Repository.RepositoryInterfaces;
using simple_online_book_catalog.Services.ServiceRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOnlineBook.Core.Test
{
    [TestFixture]
    public class BookServiceRepoTest
    {
        private Mock<IBook> moqObjBook;
        private Mock<ILogger<BookServiceRepo>> moqObjlog;
        private Mock<IMapper> moqObjMapper;
        private BookServiceRepo bookService;

        [SetUp]
        public void Setup() 
        {
            moqObjBook = new Mock<IBook>();
            moqObjlog = new Mock<ILogger<BookServiceRepo>>();
            moqObjMapper = new Mock<IMapper>();
            bookService = new BookServiceRepo(
                moqObjMapper.Object,
                moqObjBook.Object,
                moqObjlog.Object
                );
        }
        [Test]
        public async Task getAllBooksService_WithNoParameter_ShouldCallgetAllBooks() 
        {
            await bookService.getAllBooksService();
            moqObjBook.Verify(x => x.getAllBooks(), Times.Once);
        }

    }
}
