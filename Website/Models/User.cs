using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Website.Models
{
    public class User
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        [Required]
        public string Email { get; set; }
        [JsonPropertyName("isActive")]
        [Required]
        [DisplayName("Is Active ?")]
        public bool IsActive { get; set; }
    }
}
