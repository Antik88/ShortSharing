using ShortSharing.API.Dtos.ImageDtos;

namespace ShortSharing.API.Dtos.ThingDtos;

public class ShortThingDto
{
    public Guid Id { get ; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public ImageDto? Image { get; set; }
}
