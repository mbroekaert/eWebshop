using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;


namespace Website.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CategoryResponseDto[]> GetCategoriesAsync()
        {
            var httpResponse = await _httpClient.GetAsync("category");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CategoryResponseDto[]>(responseAsString);
        }

        public async Task<(bool success, string content)> CreateCategoryAsync(Category category)
        {
            var content = JsonSerializer.Serialize(category);
            var httpResponse = await _httpClient.PostAsync("category", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Category created successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }  
        public async Task<CategoryResponseDto> EditCategoryAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"category/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CategoryResponseDto>(responseAsString);
        }

        public  async Task<(bool success, string content)> UpdateCategoryAsync (Category category)
        {
            var content = JsonSerializer.Serialize(category);
            var httpResponse = await _httpClient.PutAsync($"category/{category.CategoryId}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true,"Category updated successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<CategoryResponseDto> GetCategoryToDeleteAsync (int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"category/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CategoryResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> DeleteCategoryAsync (Category category)
        {
            var httpResponse = await _httpClient.DeleteAsync($"category/{category.CategoryId}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Category deleted successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

    }
}
