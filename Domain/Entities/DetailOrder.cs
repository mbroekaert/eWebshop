using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class DetailOrder
    {
        [Key]
        public int DetailOrderId { get; set; }
        [Required]
        public Order Order { get; set; }
        [Required]
        public Product Product { get; set; }

    }
}
