using Microsoft.Extensions.Configuration;
using Shared.Contracts.Request;
using System.Text;
using System.Text.Json;

namespace Application.Auth0Users.Services
{
    public class GetAuth0ManagementTokenService
    {
        private readonly string domain;
        private readonly string clientId;
        private readonly string clientSecret;

        public GetAuth0ManagementTokenService(IConfiguration config)
        {
            this.domain = config["Auth0:Domain"];
            this.clientId = config["Auth0:Management:ClientId"];
            this.clientSecret = config["Auth0:Management:ClientSecret"];
        }

        public async Task<string> GetManagementApiAccessTokenAsync()
        {
            var _httpClient = new HttpClient();
            var request = new Auth0TokenManagementRequestDto
            {
                Audience = $"https://{domain}/api/v2/",
                ClientId = clientId,
                ClientSecret = clientSecret,
                GrantType = "client_credentials"
            };
            var content = JsonSerializer.Serialize(request);
            var httpResponse = await _httpClient.PostAsync("https://mathieubroekaert.eu.auth0.com/oauth/token", new StringContent(content, Encoding.Default, "application/json"));
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<Auth0TokenManagementResponseDto>(responseAsString);
            return deserializedResponse.AccessToken;
        }
    }
}

