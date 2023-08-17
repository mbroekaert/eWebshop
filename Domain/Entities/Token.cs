using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Token
    {

        [Key]
        public string TokenId { get; set; }
        [Required]
        public int PaymentProductId { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string ExpiryDate { get; set; }
        [Required]
        [ForeignKey(nameof(Customer))]
        public string CustomerAuth0UserId { get; set; }
    }
}
