namespace ShortSharing.API.Dtos.ImageDtos;

public class PutImageDto
{
    public string Name { get; set; } = string.Empty;

    public Guid ThingId { get; set; }
}
