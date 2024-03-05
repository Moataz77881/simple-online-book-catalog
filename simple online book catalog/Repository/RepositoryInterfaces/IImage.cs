using simple_online_book_catalog.models.DomainModel;

namespace simple_online_book_catalog.Repository.RepositoryInterfaces
{
    public interface IImage
    {
        public Task<Image> uploadImage(Image image);
    }
}
