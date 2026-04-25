using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zikrillo_Blog.BLL.Interfaces;
using Zikrillo_Blog.DAL.Interfaces;
using Zikrillo_Blog.Domain.Entites;
using Zikrillo_Blog.Shared.DTOs.Category;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _repo;
    private readonly IMapper _mapper;

    public CategoryService(IGenericRepository<Category> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    // CREATE
    public async Task<CategoryForResultDto> CreateAsync(CategoryForCreateDto dto)
    {
        var entity = _mapper.Map<Category>(dto);

        await _repo.CreateAsync(entity);
        await _repo.SaveChangesAsync();

        return _mapper.Map<CategoryForResultDto>(entity);
    }

    // UPDATE
    public async Task<CategoryForResultDto> UpdateAsync(Guid id, CategoryForUpdateDto dto)
    {
        var category = await _repo.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Category topilmadi");

        _mapper.Map(dto, category);
        category.UpdatedAt = DateTime.UtcNow;

        _repo.Update(category);
        await _repo.SaveChangesAsync();

        return _mapper.Map<CategoryForResultDto>(category);
    }

    // DELETE (soft delete)
    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _repo.GetByIdAsync(id);

        if (category == null)
            return false;

        _repo.Delete(category);
        await _repo.SaveChangesAsync();

        return true;
    }

    // GET ALL
    public async Task<IEnumerable<CategoryForResultDto>> GetAllAsync()
    {
        var categories = await _repo.GetAll()
            .OrderBy(x => x.Name)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CategoryForResultDto>>(categories);
    }

    // GET BY ID
    public async Task<CategoryForResultDto> GetByIdAsync(Guid id)
    {
        var category = await _repo.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Category topilmadi");

        return _mapper.Map<CategoryForResultDto>(category);
    }
}