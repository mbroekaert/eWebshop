using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
