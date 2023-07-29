using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public  class Customer
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [Key]
        public string Auth0UserId { get; set; }

    }
}
