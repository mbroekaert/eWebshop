using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class UserResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("named")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
    }
}
