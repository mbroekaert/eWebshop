﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string OrderReference { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public double OrderAmount { get; set; }
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public string Status { get; set; } = "Created";

        [Required]
        [ForeignKey(nameof(Customer))]
        public string CustomerAuth0UserId { get; set; }
        [Required]
        [ForeignKey(nameof(BillingAddress))]
        public int BillingAddressId { get; set; }
        [Required]
        [ForeignKey(nameof(ShippingAddress))]
        public int ShippingAddressId { get; set; }
       
    }
}
