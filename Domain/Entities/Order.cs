using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string OrderReference { get; set; }
        [Required]
        public double OrderAmount { get; set; }
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public Status Status { get; set; }
        public Payment Payment { get; set; }
        [Required]
        public Customer Customer { get; set; }
       
    }
}
