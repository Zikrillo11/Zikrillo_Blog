using Zikrillo_Blog.Shared.DTOs.User;

namespace Zikrillo_Blog.Shared.DTOs.Comment;

public class CommentForResultDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserForShortResultDto User { get; set; }
}
