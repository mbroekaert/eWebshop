﻿using Application.Common.Interfaces;
using Shared.Contracts.Request;
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
    }
}
