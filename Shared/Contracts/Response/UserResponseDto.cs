using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class UserResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

    }
}
