using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;

namespace Application.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerResponseDto> GetCustomerAsync()
        {
            var httpResponse = await _httpClient.GetAsync("customer");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CustomerResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> CreateCustomerAsync(Customer customer)
        {
            var content = JsonSerializer.Serialize(customer);
            var httpResponse = await _httpClient.PostAsync("customer", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Customer created successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }
        public async Task<CustomerResponseDto> EditCustomerAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"customer/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CustomerResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> UpdateCustomerAsync(Customer customer)
        {
            var content = JsonSerializer.Serialize(customer);
            var httpResponse = await _httpClient.PutAsync($"user/{customer.CustomerId}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Customer updated successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
