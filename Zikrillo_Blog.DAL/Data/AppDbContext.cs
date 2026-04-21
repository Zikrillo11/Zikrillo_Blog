using Microsoft.EntityFrameworkCore;
using Zikrillo_Blog.Domain.Entites;

namespace Zikrillo_Blog.DAL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
    public DbSet<User> Users{ get; set; }
}
