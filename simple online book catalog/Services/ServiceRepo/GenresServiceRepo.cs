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
        private readonly ILogger<GenresServiceRepo> logger;

        public GenresServiceRepo(IMapper mapper, IGenres genres, ILogger<GenresServiceRepo> logger)
        {
            this.mapper = mapper;
            this.genres = genres;
            this.logger = logger;
        }

        public async Task<GetGenresDTO> createGenre(CreateGenre createGenre)
        {
            logger.LogInformation("you are in createGenre serviceRepo");
            var genreCreated = await genres.CreateGenre(mapper.Map<Genres>(createGenre));
            return mapper.Map<GetGenresDTO>(genreCreated);
        }

        public async Task<GetGenresDTO?> deleteGenre(Guid id)
        {
            logger.LogInformation("you are in deleteGenre serviceRepo");

            var genreDeleted = await genres.deleteGenre(id);
            if (genreDeleted == null) return null;
            return mapper.Map<GetGenresDTO>(genreDeleted);
        }

        public async Task<List<GetGenresDTO>> getAllGenres()
        {
            logger.LogInformation("you are in getAllGenres serviceRepo");

            var domainGenres = await genres.getAllGenresAsync();
            return mapper.Map<List<GetGenresDTO>>(domainGenres);
        }

        public async Task<GetGenresDTO> updateGenre(CreateGenre updateGenre, Guid id)
        {
            logger.LogInformation("you are in updateGenre serviceRepo");

            var genreUpdated = await genres.updateGenre(id,mapper.Map<Genres>(updateGenre));
            return mapper.Map<GetGenresDTO>(genreUpdated);
        }
    }
}
