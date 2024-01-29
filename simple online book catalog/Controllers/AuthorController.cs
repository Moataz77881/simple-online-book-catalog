using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using simple_online_book_catalog.CustomActionFilter;
using simple_online_book_catalog.models.DTOModel.AuthorDTOs;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

namespace simple_online_book_catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthor authorRepo;
        private readonly IMapper mapper;

        public AuthorController(IAuthor authorRepo, IMapper mapper)
        {
            this.authorRepo = authorRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAthors() {
            //call get all Authors method
            var domain = await authorRepo.getAllAuthors();
            //map domain model to DTO model and return data
            return Ok(mapper.Map<List<getAuthorDTO>>(domain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> createAuthor([FromBody] createAuthorDTO authorDTO) {

            //map Authors DTO model to Auhtor domain model
            var domain1 = mapper.Map<Authors>(authorDTO);
            //send domain to database
            var domain2 = await authorRepo.createAuthor(domain1);
            return Ok();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> updateAuther([FromRoute]Guid id, [FromBody]createAuthorDTO authorDTO)
        {
            //map dto to domain and send it to update method
            var updatedDomain = await authorRepo.updateAuthor(id, mapper.Map<Authors>(authorDTO));
            if (updatedDomain == null) return NotFound();
            // map updatedDomain to dto to display it
            return Ok(mapper.Map<getAuthorDTO>(updatedDomain));
        }
        
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> removeAuther([FromRoute]Guid id) {
            var domain = await authorRepo.removeAuther(id);
            return Ok(mapper.Map<getAuthorDTO>(domain));
        }

    }
}
