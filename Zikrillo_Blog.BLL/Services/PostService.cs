using AutoMapper;
using Zikrillo_Blog.BLL.Interfaces;
using Zikrillo_Blog.DAL.Interfaces;
using Zikrillo_Blog.Domain.Entites;
using Zikrillo_Blog.Shared.DTOs.Post;
using Microsoft.EntityFrameworkCore;

namespace Zikrillo_Blog.BLL.Services;

public class PostService : IPostService
{
    private readonly IGenericRepository<Post> _repo;
    private readonly IMapper _mapper;

    public PostService(IGenericRepository<Post> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PostForResultDto> CreateAsync(PostForCreateDto dto, Guid userId)
    {
        var post = _mapper.Map<Post>(dto);
        post.UserId = userId;

        await _repo.CreateAsync(post);
        await _repo.SaveChangesAsync();

        return _mapper.Map<PostForResultDto>(post);
    }

    public async Task<PostForResultDto> UpdateAsync(Guid id, PostForUpdateDto dto)
    {
        var post = await _repo.GetByIdAsync(id);
        if (post == null) throw new Exception("Post topilmadi");

        _mapper.Map(dto, post);
        post.UpdatedAt = DateTime.UtcNow;

        _repo.Update(post);
        await _repo.SaveChangesAsync();

        return _mapper.Map<PostForResultDto>(post);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var post = await _repo.GetByIdAsync(id);
        if (post == null) return false;

        _repo.Delete(post);
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<PostForResultDto> GetByIdAsync(Guid id)
    {
        var post = await _repo.GetByIdAsync(id);
        if (post == null) throw new Exception("Post topilmadi");

        return _mapper.Map<PostForResultDto>(post);
    }

    public async Task<IEnumerable<PostForResultDto>> GetAllAsync()
    {
        var posts = await _repo.GetAll().ToListAsync();
        return _mapper.Map<IEnumerable<PostForResultDto>>(posts);
    }
}
