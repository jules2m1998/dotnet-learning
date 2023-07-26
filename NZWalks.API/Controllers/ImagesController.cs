using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs.ImagesDTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IImageRepository repository;

        public ImagesController(IMapper mapper, IImageRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var image = mapper.Map<Image>(request);
            image.FileExtension = Path.GetExtension(request.File.FileName);
            image.FileSizeInBytes = request.File.Length;

            var result = await repository.Upload(image);
            var response = mapper.Map<ImageDto>(result);
            return Created(response.FilePath, response);

        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtension = new[] { ".jpg", ".jpeg", "png" };
            if (!allowedExtension.Contains(Path.GetExtension(request.File.FileName)))
                ModelState.AddModelError("files", "Unsupported file extension");
            if (request.File.Length > 10485760)
                ModelState.AddModelError("files", "File size more than 10MB, please upload a smaller size file.");
        }
    }
}
