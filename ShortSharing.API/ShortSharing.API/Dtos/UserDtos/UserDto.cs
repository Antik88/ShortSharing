using System.ComponentModel.DataAnnotations;

namespace ShortSharing.API.Dtos.UserDtos;

public record UserDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    [EmailAddress]
    public required string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
