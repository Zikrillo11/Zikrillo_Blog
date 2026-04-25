namespace Zikrillo_Blog.Shared.DTOs.User;

public class UserForUpdateDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
}
