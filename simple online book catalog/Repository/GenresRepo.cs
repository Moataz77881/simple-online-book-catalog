using Microsoft.EntityFrameworkCore;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

namespace simple_online_book_catalog.Repository
{
    public class GenresRepo : IGenres
    {
        private readonly SimOnBookDbContext dbContext;
        private readonly ILogger<GenresRepo> logger;

        public GenresRepo(SimOnBookDbContext dbContext, ILogger<GenresRepo> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<Genres> CreateGenre(Genres genres)
        {
            logger.LogInformation("you are in CreateGenre repository");
            await dbContext.Genres.AddAsync(genres);
            await dbContext.SaveChangesAsync();
            return genres;
        }

        public async Task<Genres?> deleteGenre(Guid id)
        {
            logger.LogInformation("you are in deleteGenre repository");

            var domain = await dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (domain == null) return null;

            dbContext.Genres.Remove(domain);
            await dbContext.SaveChangesAsync();
            return domain;
        }

        public async Task<List<Genres>> getAllGenresAsync()
        {
            logger.LogInformation("you are in getAllGenresAsync repository");

            return await dbContext.Genres.ToListAsync();
        }

        public async Task<Genres?> updateGenre(Guid id, Genres genres)
        {
            logger.LogInformation("you are in updateGenre repository");

            var domain = await dbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (domain == null) return null;

            domain.genresOfTheBook = genres.genresOfTheBook;
            await dbContext.SaveChangesAsync();
            return domain;
        }
    }
}
