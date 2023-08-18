using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Request;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;

namespace Application.Token.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;

        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool success, string content)> CreateTokenAsync(TokenRequestDto token)
        {
            var content = JsonSerializer.Serialize(token);
            var httpResponse = await _httpClient.PostAsync("token", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Token created successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<TokenResponseDto[]> GetTokensAsync(string userId)
        {
            if (userId == null)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"token/{userId}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TokenResponseDto[]>(responseAsString);
        }

        public async Task<(bool success, string content)> DeleteTokenAsync(string tokenId)
        {
            if (string.IsNullOrEmpty(tokenId))
            {
                return (false, "Something went wrong");
            }
            var httpResponse = await _httpClient.DeleteAsync($"token/{tokenId}"); 
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Token deleted successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
