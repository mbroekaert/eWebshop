using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public  class CustomerResponseDto
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }
        [JsonPropertyName("customerFirstName")]
        public string CustomerFirstName { get; set; }
        [JsonPropertyName("customerLastName")]
        public string CustomerLastName { get; set; }
        [JsonPropertyName("customerEmail")]
        public string CustomerEmail { get; set; }
        [JsonPropertyName("customerPhone")]
        public int CustomerPhone { get; set; }
    }
}
