using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog.Core;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

namespace simple_online_book_catalog.Repository
{
    public class AuthorRepo : IAuthor
    {
        private readonly SimOnBookDbContext dbContext;
        private readonly ILogger<AuthorRepo> logger;

        public AuthorRepo(SimOnBookDbContext dbContext, ILogger<AuthorRepo> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<Authors> createAuthor(Authors authors)
        {
            logger.LogInformation("you are in createAuthor in repository");
            await dbContext.Authors.AddAsync(authors);
            await dbContext.SaveChangesAsync();
            return authors;
        }

        public async Task<List<Authors>> getAllAuthors()
        {
            logger.LogInformation("you are in getAllAuthors in repository");

            return await dbContext.Authors.ToListAsync();
        }

        public async Task<Authors?> removeAuther(Guid id)
        {
            logger.LogInformation("you are in removeAuther in repository");

            var domainData = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (domainData == null) return null;
            dbContext.Authors.Remove(domainData);
            await dbContext.SaveChangesAsync();
            return domainData;
        }

        public async Task<Authors?> updateAuthor(Guid id, Authors authors)
        {
            logger.LogInformation("you are in updateAuthor in repository");

            var existingAuthor = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (existingAuthor == null) return null;

            existingAuthor.Name = authors.Name;
            existingAuthor.photoOfTheAuthor = authors.photoOfTheAuthor;

            await dbContext.SaveChangesAsync();

            return existingAuthor;
        }
    }
}
