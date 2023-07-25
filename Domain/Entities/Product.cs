using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int ProductQuantity { get; set; }
        [Required]
        [ForeignKey(nameof(Product))]
        public int CategoryId { get; set; }

    }
}
