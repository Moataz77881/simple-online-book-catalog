using Microsoft.EntityFrameworkCore;
using simple_online_book_catalog.models.DomainModel;
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
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //modelBuilder.Entity<Authors>().HasMany<Books>(x => x.Books).WithOne(x => x.Authors)
            //  .HasForeignKey(x => x.Id);
            modelBuilder.Entity<Books>()
                .HasOne(x => x.Authors).
                WithMany(a => a.Books).
                HasForeignKey(x => x.authorId);

            modelBuilder.Entity<Books>().
                HasOne(x => x.Genres).
                WithMany(x => x.Books)
                .HasForeignKey(x=>x.genresId);
           
            modelBuilder.Entity<Books>()
                .HasOne(x => x.Image)
                .WithOne(x => x.books);
                
        }
    }
}
