using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface IDetailOrdersService
    {
        Task<DetailOrdersResponseDto[]> GetDetailsOrder(int orderId);
        Task<(bool success, string content)> DeleteDetailsOrder (int orderId);
    }
}
