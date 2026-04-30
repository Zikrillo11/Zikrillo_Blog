namespace Zikrillo_Blog.Shared.DTOs.Comment;

public class CommentForCreateDto
{
    public string Content { get; set; }   // 👈 o‘zgartirdik
    public Guid PostId { get; set; }
}