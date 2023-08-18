using Application.Common.Interfaces;
using Domain.Entities;
using OnlinePayments.Sdk.Domain;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;


namespace Website.Services
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly HttpClient _httpClient;

        public ShippingAddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ShippingAddressResponseDto[]> GetShippingAddressAsync(string userId)
        {
            var httpResponse = await _httpClient.GetAsync($"shippingAddress/user/{userId}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ShippingAddressResponseDto[]>(responseAsString);
        }

        public async Task<ShippingAddressResponseDto> GetShippingAddressById(int shippingAddressId)
        {
            if (shippingAddressId == null || shippingAddressId <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"shippingAddress/{shippingAddressId}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ShippingAddressResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> CreateShippingAddressAsync(ShippingAddress shippingAddress)
        {
            var content = JsonSerializer.Serialize(shippingAddress);
            var httpResponse = await _httpClient.PostAsync("shippingAddress", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Shipping Address created successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }  
        public async Task<ShippingAddressResponseDto> EditShippingAddressAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"shippingAddress/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ShippingAddressResponseDto>(responseAsString);
        }

        public  async Task<(bool success, string content)> UpdateShippingAddressAsync(ShippingAddress shippingAddress)
        {
            var content = JsonSerializer.Serialize(shippingAddress);
            var httpResponse = await _httpClient.PutAsync($"shippingAddress/{shippingAddress.ShippingAddressId}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Shipping Address updated successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<ShippingAddressResponseDto> GetShippingAddressToDeleteAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"shippingAddress/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ShippingAddressResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> DeleteShippingAddressAsync(ShippingAddress shippingAddress)
        {
            var httpResponse = await _httpClient.DeleteAsync($"shippingAddress/{shippingAddress.ShippingAddressId}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Shipping Address deleted successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

    }
}
