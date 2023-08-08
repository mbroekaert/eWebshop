using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IOrderService
    {
        Task<(bool success, string content)> CreateOrder(Order order, List<int> productIds, List<int> quantities);
        //Task<(bool success, string content)> UpdateOrder(Order order);
        Task<(bool success, string content)> DeleteOrderAsync(Order order);
        Task<double> CalculateTotalAmount(List<int> productIds, List<int> quantities);
        Task<(bool success, string content)> CreateOrderDetail (Order order, List<int> productIds, List<int> quantities);
    }
}
