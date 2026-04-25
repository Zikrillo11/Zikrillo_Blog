namespace Zikrillo_Blog.Shared.DTOs.Comment;

public class CommentForCreateDto
{
    public string Text { get; set; }
    public Guid PostId { get; set; }
}
