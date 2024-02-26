using Microsoft.AspNetCore.Mvc;
using simple_online_book_catalog.CustomActionFilter;
using simple_online_book_catalog.models.DTOModel.AuthorDTOs;
using simple_online_book_catalog.Services.IServices;

namespace simple_online_book_catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAthors() {
            
            return Ok(await authorService.getAllAuthorService());
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> createAuthor([FromBody] createAuthorDTO authorDTO) {

            return Ok(await authorService.createAuthor(authorDTO));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> updateAuther([FromRoute]Guid id, [FromBody]createAuthorDTO authorDTO)
        {
            var result = await authorService.updateAuthor(id, authorDTO);
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> removeAuther([FromRoute]Guid id) {

            return Ok(await authorService.removeAuther(id));
        }

    }
}
