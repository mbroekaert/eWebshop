using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public  class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public string Auth0UserId { get; set; } = "Default";
    }
}
