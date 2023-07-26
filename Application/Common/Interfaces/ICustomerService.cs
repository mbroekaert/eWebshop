using Domain.Entities;
using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> GetCustomerAsync(string auth0UserId);
        Task<(bool success, string content)> CreateCustomerAsync(Customer customer);
        Task<CustomerResponseDto> EditCustomerAsync(int id);
        Task<(bool success, string content)> UpdateCustomerAsync(Customer customer);
    }
}
