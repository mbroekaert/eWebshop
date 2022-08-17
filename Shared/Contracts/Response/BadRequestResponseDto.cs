using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class BadRequestResponseDto
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } 

        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("additionnalData")]
        public object AdditionnalData { get; set; }
    }
}
