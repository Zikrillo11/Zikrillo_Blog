namespace Zikrillo_Blog.BLL.Interfaces;

public interface IPostLikeService
{
    Task LikeAsync(Guid postId, Guid userId);
    Task UnlikeAsync(Guid postId, Guid userId);
    Task<int> CountAsync(Guid postId);
}
