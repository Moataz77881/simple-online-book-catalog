using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using simple_online_book_catalog.CustomActionFilter;
using simple_online_book_catalog.models.DTOModel.BookDTO;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

namespace simple_online_book_catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook bookRepo;
        private readonly IMapper mapper;

        public BookController(IBook bookRepo, IMapper mapper)
        {
            this.bookRepo = bookRepo;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> createBook([FromBody] CreateBookDTO book) {

            var bookCreated = await bookRepo.createNewBook(mapper.Map<Books>(book));
            return Ok(mapper.Map<CreateBookDTO>(bookCreated));
        }
        [HttpGet]
        public async Task<IActionResult> getAllBooks() {
            
            var domain = await bookRepo.getAllBooks();
            //return Ok(domain);
            return Ok(mapper.Map<List<GetBookDTO>>(domain));
        }
    }
}
