using simple_online_book_catalog.models.DTOModel.GenresDTO;

namespace simple_online_book_catalog.Services.IServices
{
    public interface IGenresService
    {
        public Task<List<GetGenresDTO>> getAllGenres();
        public Task<GetGenresDTO> createGenre(CreateGenre createGenre);
        public Task<GetGenresDTO> updateGenre(CreateGenre updateGenre, Guid id);
        public Task<GetGenresDTO?> deleteGenre(Guid id);
    }
}
