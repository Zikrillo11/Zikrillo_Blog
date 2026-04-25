using AutoMapper;
using Zikrillo_Blog.BLL.Interfaces;
using Zikrillo_Blog.DAL.Interfaces;
using Zikrillo_Blog.Domain.Entites;
using Zikrillo_Blog.Shared.DTOs.Comment;

namespace Zikrillo_Blog.BLL.Services;

public class CommentService : ICommentService
{
    private readonly IGenericRepository<Comment> _repo;
    private readonly IMapper _mapper;

    public CommentService(IGenericRepository<Comment> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CommentForResultDto> CreateAsync(CommentForCreateDto dto, Guid userId)
    {
        var comment = _mapper.Map<Comment>(dto);
        comment.UserId = userId;

        await _repo.CreateAsync(comment);
        await _repo.SaveChangesAsync();

        return _mapper.Map<CommentForResultDto>(comment);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var comment = await _repo.GetByIdAsync(id);
        if (comment == null) return false;

        _repo.Delete(comment);
        await _repo.SaveChangesAsync();
        return true;
    }
}
