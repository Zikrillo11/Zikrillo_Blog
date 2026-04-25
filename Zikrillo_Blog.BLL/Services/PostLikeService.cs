using Zikrillo_Blog.BLL.Interfaces;
using Zikrillo_Blog.DAL.Data;
using Zikrillo_Blog.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Zikrillo_Blog.BLL.Services;

public class PostLikeService : IPostLikeService
{
    private readonly AppDbContext _context;

    public PostLikeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task LikeAsync(Guid postId, Guid userId)
    {
        var exists = await _context.PostLikes
            .AnyAsync(x => x.PostId == postId && x.UserId == userId);

        if (exists)
            throw new Exception("Allaqachon like bosilgan");

        await _context.PostLikes.AddAsync(new PostLike
        {
            PostId = postId,
            UserId = userId
        });

        await _context.SaveChangesAsync();
    }

    public async Task UnlikeAsync(Guid postId, Guid userId)
    {
        var like = await _context.PostLikes
            .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

        if (like == null)
            throw new Exception("Like topilmadi");

        _context.PostLikes.Remove(like);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync(Guid postId)
    {
        return await _context.PostLikes.CountAsync(x => x.PostId == postId);
    }
}
