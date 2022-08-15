using System.Text.Json;

namespace Shared.Contracts.Response
{
    public class CategoryResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("displayOrder")]
        public int DisplayOrder { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }
    }

}