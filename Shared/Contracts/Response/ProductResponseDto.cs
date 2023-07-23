using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class ProductResponseDto
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }
        [JsonPropertyName("productReference")]
        public string ProductReference { get; set; }
        [JsonPropertyName("productPrice")]
        public double ProductPrice { get; set; }
        [JsonPropertyName("productQuantity")]
        public int ProductQuantity { get; set; }
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }
    }
}
