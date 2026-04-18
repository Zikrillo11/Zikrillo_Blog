namespace Zikrillo_Blog.Domain.Entites;

public class Comment : BaseEntity
{
    public string Text { get; set; }
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}
