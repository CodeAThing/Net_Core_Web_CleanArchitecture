using App.Application.Abstractions;

namespace App.Infrastructure;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
