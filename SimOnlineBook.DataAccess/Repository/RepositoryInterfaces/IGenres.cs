using simple_online_book_catalog.Models;

namespace simple_online_book_catalog.Repository.RepositoryInterfaces
{
    public interface IGenres
    {
        public Task<Genres> CreateGenre(Genres genres);
        public Task<List<Genres>> getAllGenresAsync();
        public Task<Genres?> updateGenre(Guid id, Genres genres);
        public Task<Genres?> deleteGenre(Guid id);
    }
}
