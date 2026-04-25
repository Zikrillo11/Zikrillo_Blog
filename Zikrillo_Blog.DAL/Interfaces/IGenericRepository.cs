using Zikrillo_Blog.Domain.Entites;

namespace Zikrillo_Blog.DAL.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll(bool asNoTracking = true);

    Task<T?> GetByIdAsync(Guid id, bool asNoTracking = true);

    Task CreateAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task SaveChangesAsync();
}
