using App.Application.Abstractions;
using App.Domain.Products;

namespace App.Application.Products;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetTopPriceProductsAsync(int count);
}
