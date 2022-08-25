using System.Text.Json.Serialization;

namespace Shared.Contracts.Request
{
    public class Auth0InitialRoleRequestDto
    {
        [JsonPropertyName("roles")]
        public string[] Roles { get; set; } = { "rol_i9BGux7B86mg1pwI" };
    }
}
