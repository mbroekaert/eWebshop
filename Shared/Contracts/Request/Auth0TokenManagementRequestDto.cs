using System.Text.Json.Serialization;

namespace Shared.Contracts.Request
{
    public class Auth0TokenManagementRequestDto
    {
        [JsonPropertyName("audience")]
        public string Audience { get; set; }
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }
        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; } = "client_credentials";
    }
}
