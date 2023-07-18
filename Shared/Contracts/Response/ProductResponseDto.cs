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
        public int productQuantity { get; set; }
        [JsonPropertyName("productPicture")]
        public string productPicture { get; set; }
        [JsonPropertyName("category")]
        public CategoryResponseDto Category { get; set; }
    }
}
