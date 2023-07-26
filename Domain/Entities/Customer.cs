using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public  class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerFirstName { get; set; }
        [Required]
        public string CustomerLastName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public int CustomerPhone { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
