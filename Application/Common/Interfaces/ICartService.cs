using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface ICartService
    {
        Task<ProductResponseDto[]> GetSpecificProductsAsync(List<int> ids);
    }
}
