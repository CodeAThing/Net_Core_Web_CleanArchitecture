using App.Repositories;
using App.Repositories.Products;

namespace App.Services;

public class ProductService(IGenericRepository<Product> productRepository)
{
    private readonly IGenericRepository<Product> _productRepository = productRepository;

    // Task A()
    // {
    //     var products = _productRepository.Where(x => true).OrderByDescending(x => x.Price).Take(5);
    // }
}
