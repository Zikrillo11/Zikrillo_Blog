using AutoMapper;
using Zikrillo_Blog.DAL.Interfaces;
using Zikrillo_Blog.Domain.Entites;
using Zikrillo_Blog.Shared.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace Zikrillo_Blog.BLL.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _repo;
    private readonly IMapper _mapper;

    public UserService(IGenericRepository<User> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<UserForResultDto> CreateAsync(UserForCreateDto dto)
    {
        var user = _mapper.Map<User>(dto);

        await _repo.CreateAsync(user);
        await _repo.SaveChangesAsync();

        return _mapper.Map<UserForResultDto>(user);
    }

    public async Task<UserForResultDto> GetByIdAsync(Guid id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) throw new Exception("User topilmadi");

        return _mapper.Map<UserForResultDto>(user);
    }

    public async Task<IEnumerable<UserForResultDto>> GetAllAsync()
    {
        var users = await _repo.GetAll().ToListAsync();
        return _mapper.Map<IEnumerable<UserForResultDto>>(users);
    }
}
