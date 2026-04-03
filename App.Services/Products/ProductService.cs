using App.Repositories;
using App.Repositories.Products;
using Microsoft.Data.SqlClient;
using System.Net;

namespace App.Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);

        var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }
    public async Task<ServiceResult<ProductDto>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            ServiceResult<ProductDto>.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
        }
        var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);
        return ServiceResult<ProductDto>.Success(productAsDto!);
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.stock
        };
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
    }
    public async Task<ServiceResult> UpdateProductAsync(int id,UpdateProductRequest request)
    {

        //fast fail
        //guard clauses: Mümkün olduğunca else kullanma.
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult.Fail("Product is not found", HttpStatusCode.NotFound);
        }
        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }
    public async Task<ServiceResult> DeleteProductAsync(int id) 
    {
        var product=await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("Product is not found", HttpStatusCode.NotFound);
        }
        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }

}
