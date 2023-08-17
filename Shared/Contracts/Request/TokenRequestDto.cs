using System.Text.Json.Serialization;

namespace Shared.Contracts.Request
{
    public class TokenRequestDto
    {
        [JsonPropertyName("tokenId")]
        public string TokenId { get; set; }
        [JsonPropertyName("paymentProductId")]
        public int? PaymentProductId { get; set; }
        [JsonPropertyName("cardNumber")]
        public string CardNumber { get; set; }
        [JsonPropertyName("expiryDate")]
        public string ExpiryDate { get; set; }
        [JsonPropertyName("customerAuth0UserId")]
        public string CustomerAuth0UserId { get; set; }

    }
}
