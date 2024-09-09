using System.Reflection.PortableExecutable;

namespace ShortSharing.API.Dtos.TypeDtos;

public record CreateTypeDto
{
    public required string Name { get; set; }
    public Guid CategoryId { get; set; }
}
