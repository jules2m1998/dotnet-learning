using NZWalks.API.Models.DTOs.DifficultyDTO;

namespace NZWalks.API.Models.DTOs.WalkDTO;

public class WalkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double LenghtInKm { get; set; }
    public string? WalkImageUrl { get; set; }

    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }

    public RegionDto Region { get; set; } = null!;
    public DifficultyDto Difficulty { get; set; } = null!;
}
