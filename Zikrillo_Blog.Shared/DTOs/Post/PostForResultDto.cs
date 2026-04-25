using Zikrillo_Blog.Shared.DTOs.Category;
using Zikrillo_Blog.Shared.DTOs.User;

namespace Zikrillo_Blog.Shared.DTOs.Post;

public class PostForResultDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? ImageUrl { get; set; }
    public int ViewCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserForShortResultDto User { get; set; }
    public CategoryForShortResultDto Category { get; set; }
}
