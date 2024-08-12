namespace User.Service.DLL.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string AuthId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? UserPictureUrl { get; set; }
}
