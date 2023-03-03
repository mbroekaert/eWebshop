using Application.Common.Interfaces;
using Domain.Entities;
using System.Text;
using System.Text.Json;

namespace Application.Auth0Users.Services
{
    public class Auth0UserService : IAuth0UserService
    {
        private readonly HttpClient _httpClient;
        
        public Auth0UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool success, string content)> CreateAuth0UserAsync(User user)
        {
            var content = JsonSerializer.Serialize(user);
            var httpResponse = await _httpClient.PostAsync("Auth0User", new StringContent(content, Encoding.Default, "application/json"));
            var result = httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, result.Result);
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());

        }

        public async Task<(bool success, string content)> DeleteAuth0UserAsync (User user)
        {
            var content = JsonSerializer.Serialize(user);
            var httpResponse = await _httpClient.PostAsync($"Auth0User/{user.Auth0UserId}", new StringContent(content, Encoding.Default, "application/json"));
            var response = httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "User deleted successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

    }
}
