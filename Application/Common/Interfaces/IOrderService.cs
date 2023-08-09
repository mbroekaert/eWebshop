using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto[]> GetOrdersAsync();
        Task<OrderResponseDto[]> GetCustomerOrders(string userId);
        Task<(bool success, string content)> CreateOrder(Order order, List<int> productIds, List<int> quantities);
        //Task<(bool success, string content)> UpdateOrder(Order order);
        Task<(bool success, string content)> DeleteOrderAsync(Order order);
        Task<double> CalculateTotalAmount(List<int> productIds, List<int> quantities);
        Task<(bool success, string content)> CreateOrderDetail (Order order, List<int> productIds, List<int> quantities);
    }
}
