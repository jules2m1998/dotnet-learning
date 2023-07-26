using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs;

public class AddRegionRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 charaters")]
    [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 charaters")]
    public string Code { get; set; } = null!;
    
    [Required]
    [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 charaters")]
    public string Name { get; set; } = null!;

    [Url(ErrorMessage = "Url must be a valid link")]
    public string? RegionImageUrl { get; set; }
}
