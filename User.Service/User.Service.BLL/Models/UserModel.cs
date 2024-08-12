namespace User.Service.BLL.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string AuthId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; }  = string.Empty;
    public string? UserPicture { get; set; }
}
