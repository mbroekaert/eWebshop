using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Payment
    {
        [Key] 
        public int PaymentId { get; set; }
        [Required]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        /* Références Worldline */
        [Required]
        public int PaymentPayid { get; set; }
        [Required]
        public string PaymentReference { get; set; }
        [Required]
        public int PaymentStatus { get; set; }

    }
}
