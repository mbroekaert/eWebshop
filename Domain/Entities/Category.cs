using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category
    {
            [Key]
            public int CategoryId { get; set; }
            [Required]
            public string CategoryName { get; set; }
            [Required]
            public int CategoryDisplayOrder { get; set; }
            [Required]
            public string CategoryDescription { get; set; }
            public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
