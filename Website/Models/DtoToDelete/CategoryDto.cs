using System.Text.Json.Serialization;

namespace Website
{
    public class CategoryDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName ("name")]
        public string Name { get; set; }
        [JsonPropertyName("displayOrder")]
        public int DisplayOrder { get; set; }
        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

    }
}
