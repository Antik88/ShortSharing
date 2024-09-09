namespace ShortSharing.API.Dtos.TypeDtos;

public record TypeDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
