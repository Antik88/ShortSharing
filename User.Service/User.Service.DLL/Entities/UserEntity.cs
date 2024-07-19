namespace User.Service.DLL.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string? AuthId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
