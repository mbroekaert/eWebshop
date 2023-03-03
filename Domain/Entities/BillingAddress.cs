using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class BillingAddress
    {
        [Key]
        public int BillingAddressId { get; set; }
        [Required]
        public string BillingAddressStreetName { get; set; }
        [Required]
        public int BillingAddressStreetNumber { get; set; }
        [Required]
        public string BillingAddressCity { get; set; }
        [Required]
        public string BillingAddressZip { get; set; }
        [Required]
        public string BillingAddressCountry { get; set; }
        [Required]
        public Customer Customer { get; set; }

    }
}