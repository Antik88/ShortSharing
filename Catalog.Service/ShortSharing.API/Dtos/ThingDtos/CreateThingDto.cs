namespace ShortSharing.API.Dtos.ThingDtos;

public class CreateThingDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required Guid CategoryId { get; set; }
    public required Guid TypeId { get; set; }
    public required Guid OwnerId { get; set; }
}
