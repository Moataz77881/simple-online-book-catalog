using simple_online_book_catalog.models.DTOModel.ImageDTO;

namespace simple_online_book_catalog.Services.IServices
{
    public interface IImageService
    {
        public Task<bool> imageUploadService(ImagesDTO image);
    }
}
