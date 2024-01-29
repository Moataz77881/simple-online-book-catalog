using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simple_online_book_catalog.CustomActionFilter;
using simple_online_book_catalog.models.DTOModel.GenresDTO;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

namespace simple_online_book_catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenres genresRepo;
        private readonly IMapper mapper;

        public GenresController(IGenres genresRepo,IMapper mapper)
        {
            this.genresRepo = genresRepo;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> createGenre([FromBody] CreateGenre genreDTO) {

            var domain = await genresRepo.CreateGenre(mapper.Map<Genres>(genreDTO));
            return Ok(domain);
        }

        [HttpGet]
        public async Task<IActionResult> getAllGenres() {

            var DomainData =  await genresRepo.getAllGenresAsync();
            return Ok(mapper.Map<List<GetGenresDTO>>(DomainData));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> updateGenres([FromRoute]Guid id, [FromBody]CreateGenre genre) {

            var domain = mapper.Map<Genres>(genre);
            var updatedGenre = await genresRepo.updateGenre(id,domain);
            if (updatedGenre == null) return NotFound();
            return Ok(mapper.Map<GetGenresDTO>(updatedGenre));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteGenre([FromRoute] Guid id) {
            
            var updatedData = await genresRepo.deleteGenre(id);
            if (updatedData == null) return NotFound();
            return Ok(mapper.Map<GetGenresDTO>(updatedData));
        }
    }
}
