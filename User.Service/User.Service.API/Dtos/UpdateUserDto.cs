namespace User.Service.API.Dtos;

public class UpdateUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserPicture { get; set; } = string.Empty;
}
