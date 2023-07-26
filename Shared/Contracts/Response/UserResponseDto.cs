using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class UserResponseDto
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("userEmail")]
        public string UserEmail { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("auth0UserId")]
        public string Auth0UserId { get; set; }

    }
}
