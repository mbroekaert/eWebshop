namespace CustomerWebsite.Models
{
    public class ShippingAddress
    {
        public int ShippingAddressId { get; set; }
        public string ShippingAddressStreetName { get; set; }
        public int ShippingAddressStreetNumber { get; set; }
        public string ShippingAddressCity { get; set; }
        public string ShippingAddressZip { get; set; }
        public string ShippingAddressCountry { get; set; }
        public Customer customer { get; set; }
    }
}
