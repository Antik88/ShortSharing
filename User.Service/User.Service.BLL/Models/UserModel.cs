﻿namespace User.Service.BLL.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string? AuthId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Connection { get; set; }
}