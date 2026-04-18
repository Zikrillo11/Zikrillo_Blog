using System.Xml.Linq;

namespace Zikrillo_Blog.Domain.Entites;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string? ImageUrl { get; set; }
    public int ViewCount { get; set; } = 0;
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<PostLike> Likes { get; set; } = new List<PostLike>();
}
