namespace User.Service.API.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string? AuthId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserPictureUrl { get; set; } = string.Empty;
};
