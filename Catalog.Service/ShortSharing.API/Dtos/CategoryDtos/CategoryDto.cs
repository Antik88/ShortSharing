using ShortSharing.API.Dtos.TypeDtos;

namespace ShortSharing.API.Dtos.CategoryDtos;

public class CategoryDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<TypeDto>? Types { get; set; }
}
