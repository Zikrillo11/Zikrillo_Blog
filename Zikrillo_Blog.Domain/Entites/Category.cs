namespace Zikrillo_Blog.Domain.Entites;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
