namespace App.Application.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
