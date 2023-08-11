using Domain.Entities;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Request
{
    public class CreateHostedCheckoutRequestDto
    {
        [JsonPropertyName("amount")]
        public double amount { get; set; }
        [JsonPropertyName("currencyCode")]
        public string currency { get; set; }
        [JsonPropertyName("orderReference")]
        public string orderReference { get; set; }
        [JsonPropertyName("returnUrl")]
        public string returnUrl { get; set; }
        [JsonPropertyName("billingAddress")]
        public BillingAddress billingAddress { get; set; } 

        [JsonPropertyName("shippingAddress")]
        public ShippingAddress shippingAddress { get; set; }

    }
}
