using simple_online_book_catalog.Data;
using simple_online_book_catalog.models.DomainModel;
using simple_online_book_catalog.Repository.RepositoryInterfaces;

namespace simple_online_book_catalog.Repository
{
    public class ImageRepo : IImage
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SimOnBookDbContext context;
        private readonly ILogger<ImageRepo> logger;

        public ImageRepo(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,
            SimOnBookDbContext context, ILogger<ImageRepo> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
            this.logger = logger;
        }


        public async Task<Image> uploadImage(Image image)
        {
            logger.LogInformation("you are in uploadImage Repository");
            //get the local path of image updoad in images folder
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.fileName}{image.fileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            var urlFilePath = $"{httpContextAccessor?.HttpContext?.Request.Scheme}://" +
                $"{httpContextAccessor?.HttpContext?.Request.Host}" +
                $"{httpContextAccessor?.HttpContext?.Request.PathBase}/Images/{image.fileName}";
            image.filePath = urlFilePath;
            await context.Images.AddAsync(image);
            await context.SaveChangesAsync();
            return image;
        }
    }
}
