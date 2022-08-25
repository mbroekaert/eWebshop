using Microsoft.Extensions.Configuration;
using Shared.Contracts.Request;
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
            var httpClient = new HttpClient();
            var request = new Auth0TokenManagementRequestDto
            {
                Audience = $"https://{domain}/api/v2/",
                ClientId = clientId,
                ClientSecret = clientSecret,
                GrantType = "client_credentials"
            };

            var httpResponse = await httpClient.PostAsync(string.Empty, new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json"));
            return JsonSerializer.Deserialize<Auth0TokenManagementResponseDto>(await httpResponse.Content.ReadAsStringAsync())?.AccessToken;
        }
    }
}

