using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class ToDoItemResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("completed")]
        public bool IsCompleted { get; set; }

    }
}
