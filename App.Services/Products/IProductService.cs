namespace App.Services.Products
{
    public interface IProductService //generic servis yazmıyoruz. Pek verimli olmuyor.
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
        Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);

        Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);

        Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
