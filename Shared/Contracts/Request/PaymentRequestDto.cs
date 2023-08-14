using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Request
{
    public class PaymentRequestDto
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
