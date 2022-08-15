using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class RequestValidatorResponseDto
    {
        [JsonPropertyName("validations")]
        public Dictionary<string, string> Validations { get; set; }
    }
}
