using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class RequestValidatorResponseDto
    {
        [JsonPropertyName("validations")]
        public Dictionary<string, List<string>> Validations { get; set; }
    }
}
