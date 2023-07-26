using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly NZWalksDbContext dbContext;
    private readonly IWebHostEnvironment environment;
    private readonly IHttpContextAccessor accessor;

    public ImageRepository(NZWalksDbContext dbContext, IWebHostEnvironment environment, IHttpContextAccessor accessor)
    {
        this.dbContext = dbContext;
        this.environment = environment;
        this.accessor = accessor;
    }

    public async Task<Image> Upload(Image image)
    {
        var localFilrPath = Path.Combine(environment.ContentRootPath, "Images", image.FileName + image.FileExtension);
        using var stream = new FileStream(localFilrPath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        var urlFilePath = $"{accessor.HttpContext?.Request.Scheme}://{accessor.HttpContext?.Request.Host}{accessor.HttpContext?.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
        image.FilePath = urlFilePath;

        await dbContext.Images.AddAsync(image);
        await dbContext.SaveChangesAsync();
        return image;
    }
}
