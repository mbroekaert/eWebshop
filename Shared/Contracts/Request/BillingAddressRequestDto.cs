using System.Text.Json.Serialization;

namespace Shared.Contracts.Request
{
    public class BillingAddressRequestDto
    {
        [JsonPropertyName("billingAddressId")]
        public int billingAddressId { get; set; }
        [JsonPropertyName("billingAddressStreetName")]
        public string billingAddressStreetName { get; set; }
        [JsonPropertyName("billingAddressStreetNumber")]
        public int billingAddressStreetNumber { get; set; }
        [JsonPropertyName("billingAddressCity")]
        public string billingAddressCity { get; set; }
        [JsonPropertyName("billingAddressZip")]
        public string billingAddressZip { get; set; }
        [JsonPropertyName("billingAddressCountry")]
        public string billingAddressCountry { get; set; }
        
    }
}
