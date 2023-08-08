using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class DetailOrderResponseDto
    {
        [JsonPropertyName("detailOrderId")]
        public int DetailOrderId { get; set; }
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
