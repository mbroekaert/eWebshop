using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Contracts.Request
{
    public class RefundRequestDto
    {
        [JsonPropertyName("paymentId")]
        public string PaymentPayid { get; set; }
        [JsonPropertyName("orderAmount")]
        public double OrderAmount { get; set; }
        [JsonPropertyName("orderReference")]
        public string OrderReference { get; set; }

    }
}
