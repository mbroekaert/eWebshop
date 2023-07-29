using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class BillingAddressResponseDto
    {
        [JsonPropertyName("billingAddressId")]
        public int BillingAddressId { get; set; }
        [JsonPropertyName("billingAddressStreetName")]
        public string BillingAddressStreetName { get; set; }
        [JsonPropertyName("billingAddressStreetNumber")]
        public int BillingAddressStreetNumber { get; set; }
        [JsonPropertyName("billingAddressCity")]
        public string BillingAddressCity { get; set; }
        [JsonPropertyName("billingAddressZip")]
        public string BillingAddressZip { get; set; }
        [JsonPropertyName("billingAddressCountry")]
        public string BillingAddressCountry { get; set; }
        [JsonPropertyName("customerAuth0UserId")]
        public string CustomerAuth0UserId { get; set; }
    }
}
