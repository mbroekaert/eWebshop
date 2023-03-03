using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductReference { get; set; }
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public int productQuantity { get; set; }
        [Required]
        public string productPicture { get; set; }
        [Required]
        public Category Category { get; set; }

    }
}
