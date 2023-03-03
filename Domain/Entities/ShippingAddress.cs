using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class ShippingAddress
    {
        [Key]
        public int ShippingAddressId { get; set; }
        [Required]
        public string ShippingAddressStreetName { get; set; }
        [Required]
        public int ShippingAddressStreetNumber { get; set; }
        [Required]
        public string ShippingAddressCity { get; set; }
        [Required]
        public string ShippingAddressZip { get; set; }
        [Required]
        public string ShippingAddressCountry { get; set; }
        [Required]
        public Customer Customer { get; set; }
        
    }
}
