using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;

namespace Application.Users.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponseDto[]> GetUsersAsync()
        {
            var httpResponse = await _httpClient.GetAsync("user");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserResponseDto[]>(responseAsString);
        }

        public async Task<(bool success, string content)> CreateUserAsync(User user)
        {
            var content = JsonSerializer.Serialize(user);
            var httpResponse = await _httpClient.PostAsync("user", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "User created successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }
        public async Task<UserResponseDto> EditUserAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"user/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> UpdateUserAsync(User user)
        {
            var content = JsonSerializer.Serialize(user);
            var httpResponse = await _httpClient.PutAsync($"user/{user.Id}", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "User updated successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<UserResponseDto> GetUserToDeleteAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"user/{id}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserResponseDto>(responseAsString);
        }

        public async Task<(bool success, string content)> DeleteUserAsync(User user)
        {
            var httpResponse = await _httpClient.DeleteAsync($"category/{user.Id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "User deleted successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
