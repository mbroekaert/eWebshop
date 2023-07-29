using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class ShippingAddressResponseDto
    {
        [JsonPropertyName("shippingAddressId")]
        public int ShippingAddressId { get; set; }
        [JsonPropertyName("shippingAddressStreetName")]
        public string ShippingAddressStreetName { get; set; }
        [JsonPropertyName("shippingAddressStreetNumber")]
        public int ShippingAddressStreetNumber { get; set; }
        [JsonPropertyName("shippingAddressCity")]
        public string ShippingAddressCity { get; set; }
        [JsonPropertyName("shippingAddressZip")]
        public string ShippingAddressZip { get; set; }
        [JsonPropertyName("shippingAddressCountry")]
        public string ShippingAddressCountry { get; set; }
        [JsonPropertyName("shippingAuth0UserId")]
        public string CustomerAuth0UserId { get; set; }
    }
}
