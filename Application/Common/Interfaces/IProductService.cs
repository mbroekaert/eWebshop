using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDto[]> GetProductsAsync();
        Task<(bool success, string content)> CreateProductAsync(Product product);
        Task<ProductResponseDto> EditProductAsync(int id);
        Task<(bool success, string content)> UpdateProductAsync(Product product);
        Task<ProductResponseDto> GetProductToDeleteAsync(int id);
        Task<(bool success, string content)> DeleteProductAsync(Product product);

    }
}
