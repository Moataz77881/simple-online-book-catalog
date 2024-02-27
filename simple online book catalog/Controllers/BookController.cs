using Microsoft.AspNetCore.Mvc;
using simple_online_book_catalog.CustomActionFilter;
using simple_online_book_catalog.models.DTOModel.BookDTO;
using simple_online_book_catalog.Services.IServices;

namespace simple_online_book_catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> createBook([FromBody] CreateBookDTO book) {

            return Ok(await bookService.createBookService(book));
        }
        [HttpGet]
        public async Task<IActionResult> getAllBooks() {
            
            return Ok(await bookService.getAllBooksService());
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> updateBook([FromRoute] Guid id,[FromBody] CreateBookDTO updateBook) {

            return Ok(await bookService.updatebookService(updateBook, id));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteBook([FromRoute] Guid id) {

            return Ok(await bookService.deleteBook(id));
        }
    }
}
