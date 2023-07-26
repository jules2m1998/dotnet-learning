using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs.ImagesDTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;
        [Required]
        public string FileName { get; set; } = null!;
        public string? FileDescription { get; set; }
    }
}
