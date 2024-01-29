using Microsoft.EntityFrameworkCore;
using simple_online_book_catalog.Models;

namespace simple_online_book_catalog.Data
{
    public class SimOnBookDbContext : DbContext
    {
        public SimOnBookDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Genres> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Authors>().HasMany<Books>(x => x.Books).WithOne(x => x.Authors)
                .HasForeignKey(x => x.authorId);

            modelBuilder.Entity<Genres>().HasMany<Books>(x => x.Books).WithOne(x => x.Genres)
                .HasForeignKey(x => x.genresId);
        }
    }
}
