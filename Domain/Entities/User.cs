using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public  class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public string Password { get; set; }
    }
}
