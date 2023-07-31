using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Response;
using System.Net.Http;
using System.Text.Json;

namespace Application.Cart.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductResponseDto[]> GetSpecificProductsAsync(List<int> ids)
        {
            var queryString = string.Join("&", ids.Select(id => $"productIds={id}"));
            var httpResponse = await _httpClient.GetAsync($"cart?{queryString}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductResponseDto[]>(responseAsString);
        }
        

    }
}
