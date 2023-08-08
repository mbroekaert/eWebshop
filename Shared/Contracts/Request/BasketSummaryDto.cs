using Shared.Contracts.Response;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Request
{
    public class BasketSummaryDto
    {
        [JsonPropertyName("productResponseDto")]
        public ProductResponseDto[] ProductResponseDtos { get; set; }
        [JsonPropertyName("billingAddressResponseDto")]
        public BillingAddressResponseDto[] BillingAddressResponseDtos { get; set; }
        [JsonPropertyName("shippingAddressResponseDto")]
        public ShippingAddressResponseDto[] shippingAddressResponseDtos { get; set; }
    }
}
