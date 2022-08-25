using System.Text.Json.Serialization;

namespace Shared.Contracts.Request
{
    public  class Auth0UserRequestDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
       
        [JsonPropertyName("connection")]
        public string Connection { get; set; } = "Username-Password-Authentication";

        [JsonPropertyName("password")]
        public string Password { get; set; } = "Test123!";

    }
}
