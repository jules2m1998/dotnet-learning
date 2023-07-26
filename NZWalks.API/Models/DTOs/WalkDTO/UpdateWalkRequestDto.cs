namespace NZWalks.API.Models.DTOs.WalkDTO;

public class UpdateWalkRequestDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double LenghtInKm { get; set; }
    public string? WalkImageUrl { get; set; }

    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
}
