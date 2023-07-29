namespace CustomerWebsite.Models
{
    public class BillingAddress
    {
        public int BillingAddressId { get; set; }
        public string BillingAddressStreetName { get; set; }
        public int BillingAddressStreetNumber { get; set; }
        public string BillingAddressCity { get; set; }
        public string BillingAddressZip { get; set; }
        public string BillingAddressCountry { get; set; }
        public Customer customer { get; set; }
    }
}
