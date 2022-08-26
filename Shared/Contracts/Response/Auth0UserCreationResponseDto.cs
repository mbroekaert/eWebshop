using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public  class Auth0UserCreationResponseDto
    {
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set;}
        [JsonPropertyName("email")]
        public string Email { get; set;}
        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set;}
        [JsonPropertyName("name")]
        public string Name { get; set;}
        [JsonPropertyName("nickname")]
        public string NickName { get; set;}
        [JsonPropertyName("picture")]
        public string picture { get; set;}
        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set;}
        [JsonPropertyName("user_id")]
        public string UserId { get; set;}

    }
}
