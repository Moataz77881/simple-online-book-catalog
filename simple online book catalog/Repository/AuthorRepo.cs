using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simple_online_book_catalog.Data;
using simple_online_book_catalog.Models;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

namespace simple_online_book_catalog.Repository
{
    public class AuthorRepo : IAuthor
    {
        private readonly SimOnBookDbContext dbContext;

        public AuthorRepo(SimOnBookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Authors> createAuthor(Authors authors)
        {
            await dbContext.Authors.AddAsync(authors);
            await dbContext.SaveChangesAsync();
            return authors;
        }

        public async Task<List<Authors>> getAllAuthors()
        {
            return await dbContext.Authors.ToListAsync();
        }

        public async Task<Authors?> removeAuther(Guid id)
        {
            var domainData = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (domainData == null) return null;
            dbContext.Authors.Remove(domainData);
            await dbContext.SaveChangesAsync();
            return domainData;
        }

        public async Task<Authors?> updateAuthor(Guid id, Authors authors)
        {
            var existingAuthor = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (existingAuthor == null) return null;

            existingAuthor.Name = authors.Name;
            existingAuthor.photoOfTheAuthor = authors.photoOfTheAuthor;

            await dbContext.SaveChangesAsync();

            return existingAuthor;
        }
    }
}
