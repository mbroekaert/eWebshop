using Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerWebsite.Models
{
    public class Order
    {
        [Key]
        [DisplayName("Id")]
        public int OrderId { get; set; }
        [Required]
        [DisplayName("Order reference")]
        public string OrderReference { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [DisplayName("Total amount")]
        public double OrderAmount { get; set; }
        [Required]
        [DisplayName("Order date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Status")]
        public string Status { get; set; } = "Created";
        [DisplayName("Payment Id")]
        public Payment Payment { get; set; }
        [Required]
        [DisplayName("Customer Id")]
        public Customer Customer { get; set; }
        [Required]
        [DisplayName("Billing address Id")]
        public int BillingAddressId { get; set; }
        [Required]
        [DisplayName("Shipping address Id")]
        public int ShippingAddressId { get; set; }
    }
}
