using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using simple_online_book_catalog.models.DTOModel.GenresDTO;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository.RepositoryInterfaces;
using simple_online_book_catalog.Services.IServices;

namespace simple_online_book_catalog.Services.ServiceRepo
{
    public class GenresServiceRepo : IGenresService
    {
        private readonly IMapper mapper;
        private readonly IGenres genres;

        public GenresServiceRepo(IMapper mapper, IGenres genres)
        {
            this.mapper = mapper;
            this.genres = genres;
        }

        public async Task<GetGenresDTO> createGenre(CreateGenre createGenre)
        {
            var genreCreated = await genres.CreateGenre(mapper.Map<Genres>(createGenre));
            return mapper.Map<GetGenresDTO>(genreCreated);
        }

        public async Task<GetGenresDTO?> deleteGenre(Guid id)
        {
            var genreDeleted = await genres.deleteGenre(id);
            if (genreDeleted == null) return null;
            return mapper.Map<GetGenresDTO>(genreDeleted);
        }

        public async Task<List<GetGenresDTO>> getAllGenres()
        {
            var domainGenres = await genres.getAllGenresAsync();
            return mapper.Map<List<GetGenresDTO>>(domainGenres);
        }

        public async Task<GetGenresDTO> updateGenre(CreateGenre updateGenre, Guid id)
        {
            var genreUpdated = await genres.updateGenre(id,mapper.Map<Genres>(updateGenre));
            return mapper.Map<GetGenresDTO>(genreUpdated);
        }
    }
}
