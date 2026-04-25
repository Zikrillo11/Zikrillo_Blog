using Microsoft.EntityFrameworkCore;
using Zikrillo_Blog.DAL.Data;
using Zikrillo_Blog.DAL.Interfaces;
using Zikrillo_Blog.Domain.Entites;

namespace Zikrillo_Blog.DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _db;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _db = _context.Set<T>();
    }

    public IQueryable<T> GetAll(bool asNoTracking = true)
    {
        return asNoTracking ? _db.AsNoTracking() : _db;
    }

    public async Task<T?> GetByIdAsync(Guid id, bool asNoTracking = true)
    {
        var query = _db.Where(x => x.Id == id);

        return asNoTracking
            ? await query.AsNoTracking().FirstOrDefaultAsync()
            : await query.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await _db.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _db.Update(entity);
    }

    public void Delete(T entity)
    {
        // Soft delete ishlatmoqchi bo‘lsang:
        entity.IsDeleted = true;
        _db.Update(entity);

        // Agar hard delete bo‘lsa:
        // _db.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
