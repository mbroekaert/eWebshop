using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Payment
    {
        [Key] 
        public int PaymentId { get; set; }

        /* Références Worldline */
        [Required]
        public int PaymentPayid { get; set; } 
        [Required]
        public string PaymentReference { get; set; }
        [Required]
        public int PaymentStatus { get; set; }


    }
}
