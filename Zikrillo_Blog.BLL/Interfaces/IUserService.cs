using Zikrillo_Blog.Shared.DTOs.User;
public interface IUserService
{
    Task<UserForResultDto> CreateAsync(UserForCreateDto dto);
    Task<UserForResultDto> GetByIdAsync(Guid id);
    Task<IEnumerable<UserForResultDto>> GetAllAsync();
}
