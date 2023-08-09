using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Contracts.Response
{
    public class OrderResponseDto
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("orderReference")]
        public string OrderReference { get; set; } = Guid.NewGuid().ToString();
        [JsonPropertyName("orderAmount")]
        public double OrderAmount { get; set; }
        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("paymentId")]
        public int PaymentId { get; set; }
        [JsonPropertyName("customerAuth0UserId")]
        public string CustomerAuth0UserId { get; set; }
        [JsonPropertyName("BillingAddressId")]
        public int BillingAddressId { get; set; }
        [JsonPropertyName("shippingAddressId")]
        public int ShippingAddressId { get; set; }
    }
}
