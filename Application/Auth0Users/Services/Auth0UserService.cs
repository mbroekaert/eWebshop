using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Request;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Application.Auth0Users.Services
{
    public class Auth0UserService : IAuth0UserService
    {
        private readonly HttpClient _httpClient;
        //private readonly GetAuth0ManagementTokenService service;
        public Auth0UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool success, string content)> CreateAuth0UserAsync(User user)
        {
            var content = JsonSerializer.Serialize(user);
            var httpResponse = await _httpClient.PostAsync("Auth0User", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "User created successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());

        }
    }


}
