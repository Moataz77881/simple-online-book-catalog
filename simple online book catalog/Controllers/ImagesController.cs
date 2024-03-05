using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simple_online_book_catalog.models.DTOModel.ImageDTO;
using simple_online_book_catalog.Repository.RepositoryInterfaces;
using simple_online_book_catalog.Services.IServices;

namespace simple_online_book_catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> uploadImage([FromForm] ImagesDTO request) 
        {
            validateFileUpload(request);
            if (ModelState.IsValid) 
            {
                if (!await imageService.imageUploadService(request))
                    return BadRequest("something was wrong");
                else return Ok("Image Uploaded");
            }
            return BadRequest(ModelState);

        }
        private void validateFileUpload(ImagesDTO image)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg",".png"};
            if (!allowedExtentions.Contains(Path.GetExtension(image.File.FileName))) 
            {
                ModelState.AddModelError("file", "Unsupported file extentions");
            }
            if (image.File.Length > 10435760) 
            {
                ModelState.AddModelError("file", "File size more than 10 MB, please upload a smaller file size");
            }
        }
    }
}
