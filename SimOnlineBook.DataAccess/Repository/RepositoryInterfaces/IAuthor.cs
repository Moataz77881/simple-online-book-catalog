using simple_online_book_catalog.Models;

namespace simple_online_book_catalog.Repository.RepositoryInterfaces
{
    public interface IAuthor
    {
        Task<List<Authors>> getAllAuthors();
        Task<Authors> createAuthor(Authors authors);
        Task<Authors?> updateAuthor(Guid id, Authors authors);
        Task<Authors?> removeAuther(Guid id);
    }
}
