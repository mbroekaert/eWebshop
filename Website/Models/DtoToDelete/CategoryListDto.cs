using System.Text.Json.Serialization;

namespace Website.Models.Dto
{
    public class CategoryListDto
    {
        [JsonPropertyName("lists")]
        public List<CategoryDto> Categories { get; set; }
    }
}
