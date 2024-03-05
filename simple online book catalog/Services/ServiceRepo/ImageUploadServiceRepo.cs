using simple_online_book_catalog.models.DomainModel;
using simple_online_book_catalog.models.DTOModel.ImageDTO;
using simple_online_book_catalog.Repository;
using simple_online_book_catalog.Repository.RepositoryInterfaces;
using simple_online_book_catalog.Services.IServices;

namespace simple_online_book_catalog.Services.ServiceRepo
{
    public class ImageUploadServiceRepo : IImageService
    {
        private readonly IImage imageRepoOpj;
        private readonly ILogger<ImageUploadServiceRepo> logger;

        public ImageUploadServiceRepo(IImage imageRepoOpj, ILogger<ImageUploadServiceRepo> logger)
        {
            this.imageRepoOpj = imageRepoOpj;
            this.logger = logger;
        }
        public async Task<bool> imageUploadService(ImagesDTO image)
        {
            logger.LogInformation("you are in imageUploadService in serviceRepo");
            var imageDomain = new Image
            {
                File = image.File,
                fileDescription = image.fileDescription,
                fileName = image.fileName,
                fileExtension = Path.GetExtension(image.File.FileName),
                fileSize = image.File.Length
            };
            var imageUploaded = await imageRepoOpj.uploadImage(imageDomain);
            if (imageUploaded == null) {
                return false;
            }
            return true;
        }
    }
}
