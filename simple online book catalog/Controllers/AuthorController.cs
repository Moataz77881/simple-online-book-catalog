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
        private readonly ILogger<AuthorController> logger;

        public AuthorController(IAuthorService authorService,ILogger<AuthorController> logger)
        {
            this.authorService = authorService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors() 
        {
            logger.LogInformation("Using GetAllAuthors Action method");
            var respons = await authorService.getAllAuthorService();
            logger.LogInformation("Call Succeeded");
            return Ok(respons);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> createAuthor([FromBody] createAuthorDTO authorDTO) 
        {
            logger.LogInformation("you are in createAuthor Action method");
            return Ok(await authorService.createAuthor(authorDTO));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> updateAuthor([FromRoute]Guid id, [FromBody]createAuthorDTO authorDTO)
        {
            logger.LogInformation("you are in updateAuthor Action method");
            var result = await authorService.updateAuthor(id, authorDTO);
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> removeAuthor([FromRoute]Guid id) 
        {
            logger.LogInformation("you are in removeAuthor Action method");
            return Ok(await authorService.removeAuthor(id));
        }

    }
}
