using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DetailOrder
    {
        [Key]
        public int DetailOrderId { get; set; }
        [Required]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
