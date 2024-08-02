namespace ShortSharing.API.Dtos.ThingDtos; 

public record ThingDto
{
    public Guid Id { get ; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid OwnerId { get; set; }
}
