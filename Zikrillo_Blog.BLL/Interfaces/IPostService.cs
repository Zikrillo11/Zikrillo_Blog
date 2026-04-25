using Zikrillo_Blog.Shared.DTOs.Post;

namespace Zikrillo_Blog.BLL.Interfaces;

public interface IPostService
{
    Task<PostForResultDto> CreateAsync(PostForCreateDto dto, Guid userId);
    Task<PostForResultDto> UpdateAsync(Guid id, PostForUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<PostForResultDto> GetByIdAsync(Guid id);
    Task<IEnumerable<PostForResultDto>> GetAllAsync();
}
