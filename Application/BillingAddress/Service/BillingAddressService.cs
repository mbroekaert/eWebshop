using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;


namespace Website.Services
{
    public class BillingAddressService : IBillingAddressService
    {
        private readonly HttpClient _httpClient;

        public BillingAddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BillingAddressResponseDto[]> GetBillingAddressAsync(string userId)
        {
            if (userId == null)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"billingAddress/user/{userId}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BillingAddressResponseDto[]>(responseAsString);
        }

        public async Task<BillingAddressResponseDto> GetBillingAddressById(int BillingAddressId)
        {
            if (BillingAddressId == null || BillingAddressId <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"billingAddress/{BillingAddressId}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BillingAddressResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> CreateBillingAddressAsync(BillingAddress billingAddress)
        {
            var content = JsonSerializer.Serialize(billingAddress);
            var httpResponse = await _httpClient.PostAsync("billingAddress", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Billing Address created successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }  
        public async Task<BillingAddressResponseDto> EditBillingAddressAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"billingAddress/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BillingAddressResponseDto>(responseAsString);
        }

        public  async Task<(bool success, string content)> UpdateBillingAddressAsync(BillingAddress billingAddress)
        {
            var content = JsonSerializer.Serialize(billingAddress);
            var httpResponse = await _httpClient.PutAsync($"billingAddress/{billingAddress.BillingAddressId}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Billing Address updated successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<BillingAddressResponseDto> GetBillingAddressToDeleteAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"billingAddress/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BillingAddressResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> DeleteBillingAddressAsync(BillingAddress billingAddress)
        {
            var httpResponse = await _httpClient.DeleteAsync($"billingAddress/{billingAddress.BillingAddressId}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Billing Address deleted successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

    }
}
