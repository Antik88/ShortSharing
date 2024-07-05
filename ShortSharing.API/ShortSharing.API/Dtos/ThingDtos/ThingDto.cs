namespace ShortSharing.API.Dtos.ThingDtos; 

public record ThingDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
