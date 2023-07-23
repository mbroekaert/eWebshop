using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;


namespace Website.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductResponseDto[]> GetProductsAsync()
        {
            var httpResponse = await _httpClient.GetAsync("product");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductResponseDto[]>(responseAsString);
        }

        public async Task<(bool success, string content)> CreateProductAsync(Product product)
        {
            var content = JsonSerializer.Serialize(product);
            var httpResponse = await _httpClient.PostAsync("product", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Product created successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }  
        public async Task<ProductResponseDto> EditProductAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"product/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductResponseDto>(responseAsString);
        }

        public  async Task<(bool success, string content)> UpdateProductAsync (Product product)
        {
            var content = JsonSerializer.Serialize(product);
            var httpResponse = await _httpClient.PutAsync($"product/{product.ProductId}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true,"Product updated successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<ProductResponseDto> GetProductToDeleteAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"product/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> DeleteProductAsync(Product product)
        {
            var httpResponse = await _httpClient.DeleteAsync($"product/{product.ProductId}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Product deleted successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

    }
}
