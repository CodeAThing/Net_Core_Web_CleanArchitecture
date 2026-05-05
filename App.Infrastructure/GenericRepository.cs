using App.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext Context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public Task<List<T>> GetAllAsync() => _dbSet.AsNoTracking().ToListAsync();

    public Task<List<T>> GetPagedAsync(int skip, int take)
    {
        return _dbSet.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    }

    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
