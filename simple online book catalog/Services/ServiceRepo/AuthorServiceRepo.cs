using AutoMapper;
using simple_online_book_catalog.models.DTOModel.AuthorDTOs;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository;
using simple_online_book_catalog.Repository.RepositoryInterfaces;
using simple_online_book_catalog.Services.IServices;

namespace simple_online_book_catalog.IServices.Services
{
    public class AuthorServiceRepo : IAuthorService
    {
        private readonly IAuthor author;
        private readonly IMapper mapper;

        public AuthorServiceRepo(IAuthor author, IMapper mapper)
        {
            this.author = author;
            this.mapper = mapper;
        }

        public async Task<getAuthorDTO> createAuthor(createAuthorDTO authorDTO)
        {
            //map Authors DTO model to Auhtor domain model
            var domain1 = mapper.Map<Authors>(authorDTO);
            //send domain to database
            var domain2 = await author.createAuthor(domain1);
            // convert to dto
            return mapper.Map<getAuthorDTO>(domain2);
        }

        public async Task<List<getAuthorDTO>> getAllAuthorService()
        {
            var domain = await author.getAllAuthors();
            var dto = mapper.Map<List<getAuthorDTO>>(domain);
            return dto;
        }

        public async Task<getAuthorDTO> removeAuther(Guid id)
        {
            var domain = await author.removeAuther(id);
            return mapper.Map<getAuthorDTO>(domain);
        }

        public async Task<getAuthorDTO?> updateAuthor(Guid id, createAuthorDTO createAuthorDTO)
        {
            //map dto to domain and send it to update method
            var updatedDomain = await author.updateAuthor(id, mapper.Map<Authors>(createAuthorDTO));
            if (updatedDomain == null) return null;
            // map updatedDomain to dto to display it
            return mapper.Map<getAuthorDTO>(updatedDomain);
        }
    }
}
