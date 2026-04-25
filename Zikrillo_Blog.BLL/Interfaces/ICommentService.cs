using Zikrillo_Blog.Shared.DTOs.Comment;

namespace Zikrillo_Blog.BLL.Interfaces;
public interface ICommentService
{
    Task<CommentForResultDto> CreateAsync(CommentForCreateDto dto, Guid userId);
    Task<bool> DeleteAsync(Guid id);
}
