using App.Application;
using App.Application.Abstractions;
using App.Domain.Products;
using System.Net;

namespace App.Application.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        if (count <= 0)
        {
            return ServiceResult<List<ProductDto>>.Fail("Count must be greater than zero");
        }

        var products = await productRepository.GetTopPriceProductsAsync(count);
        var productsAsDto = products.Select(MapToDto).ToList();

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var products = await productRepository.GetAllAsync();
        var productsAsDto = products.Select(MapToDto).ToList();

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return ServiceResult<List<ProductDto>>.Fail("Page number and page size must be greater than zero");
        }

        var skip = (pageNumber - 1) * pageSize;
        var products = await productRepository.GetPagedAsync(skip, pageSize);
        var productsAsDto = products.Select(MapToDto).ToList();

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
        }

        return ServiceResult<ProductDto?>.Success(MapToDto(product));
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
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

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult.Fail("Product is not found", HttpStatusCode.NotFound);
        }

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto(product.Id, product.Name, product.Price, product.Stock);
    }
}
