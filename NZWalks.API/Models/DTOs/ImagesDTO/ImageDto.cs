namespace NZWalks.API.Models.DTOs.ImagesDTO;

public class ImageDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = null!;
    public string? FileDescription { get; set; }
    public string FileExtension { get; set; } = null!;
    public long FileSizeInBytes { get; set; }
    public string FilePath { get; set; } = null!;
}
