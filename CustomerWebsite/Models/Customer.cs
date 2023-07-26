namespace CustomerWebsite.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        
        public string CustomerFirstName { get; set; }
       
        public string CustomerLastName { get; set; }
        
        public string CustomerEmail { get; set; }
        
        public int CustomerPhone { get; set; }
        public string Password { get; set; }
    }
}
