using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [ForeignKey(nameof(Customer))]
        public string CustomerAuth0UserId { get; set; }

    }
}