namespace Zikrillo_Blog.Domain.Entites;

public class PostLike : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid PostId { get; set; }
    public Post Post { get; set; }
}
