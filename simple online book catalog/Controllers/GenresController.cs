using Microsoft.AspNetCore.Mvc;
using simple_online_book_catalog.CustomActionFilter;
using simple_online_book_catalog.models.DTOModel.GenresDTO;
using simple_online_book_catalog.Services.IServices;

namespace simple_online_book_catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> createGenre([FromBody] CreateGenre genreDTO) {

            return Ok(await genresService.createGenre(genreDTO));
        }

        [HttpGet]
        public async Task<IActionResult> getAllGenres() {

            return Ok(await genresService.getAllGenres());
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> updateGenres([FromRoute]Guid id, [FromBody]CreateGenre genre) {

            return Ok(await genresService.updateGenre(genre, id));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteGenre([FromRoute] Guid id) {
         
            var genreDeleted = await genresService.deleteGenre(id);
            if(genreDeleted == null) return NotFound();
            return Ok("Genre deleted"); 
        }
    }
}
