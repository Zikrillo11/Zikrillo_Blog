namespace Zikrillo_Blog.Shared.DTOs.Post;

public class PostForCreateDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string? ImageUrl { get; set; }
    public Guid CategoryId { get; set; }
}
