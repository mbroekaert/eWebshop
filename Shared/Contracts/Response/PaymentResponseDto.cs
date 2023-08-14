using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Contracts.Response
{
    public class PaymentResponseDto
    {
        [JsonPropertyName("paymentId")]
        public int PaymentId { get; set; }
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        /* Références Worldline */
        [JsonPropertyName("paymentPayid")]
        public string PaymentPayid { get; set; }
        [JsonPropertyName("paymentReference")]
        public string PaymentReference { get; set; }
        [JsonPropertyName("paymentStatus")]
        public int? PaymentStatus { get; set; }
    }
}
