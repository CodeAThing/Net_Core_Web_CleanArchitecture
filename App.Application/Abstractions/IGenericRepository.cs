namespace App.Application.Abstractions;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetPagedAsync(int skip, int take);
    ValueTask<T?> GetByIdAsync(int id);
    ValueTask AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
