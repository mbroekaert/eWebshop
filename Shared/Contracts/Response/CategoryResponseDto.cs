using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class CategoryResponseDto
    {
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }
        [JsonPropertyName("categoryDescription")]
        public string CategoryDescription { get; set; }

        [JsonPropertyName("categoryDisplayOrder")]
        public int CategoryDisplayOrder { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }
    }
}
