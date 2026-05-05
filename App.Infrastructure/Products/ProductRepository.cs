using App.Application.Products;
using App.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Products;

public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    public Task<List<Product>> GetTopPriceProductsAsync(int count)
    {
        return Context.Products
            .AsNoTracking()
            .OrderByDescending(x => x.Price)
            .Take(count)
            .ToListAsync();
    }
}
