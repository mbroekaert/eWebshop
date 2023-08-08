using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerWebsite.Models
{
    public class DetailOrder
    {

        [Key]
        [DisplayName("Detail Order Id")]
        public int DetailOrderId { get; set; }
        [Required]
        [DisplayName("Order Id")]
        public Order Order { get; set; }
        [Required]
        [DisplayName("Product Id")]
        public Product Product { get; set; }
        [Required]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
    }
}
