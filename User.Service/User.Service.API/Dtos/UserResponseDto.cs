namespace User.Service.API.Dtos;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string? AuthId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
