using Zikrillo_Blog.Shared.DTOs.Category;

namespace Zikrillo_Blog.BLL.Interfaces;

public interface ICategoryService
{
    Task<CategoryForResultDto> CreateAsync(CategoryForCreateDto dto);
    Task<CategoryForResultDto> UpdateAsync(Guid id, CategoryForUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<CategoryForResultDto>> GetAllAsync();
    Task<CategoryForResultDto> GetByIdAsync(Guid id);
}
