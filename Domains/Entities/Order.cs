using System;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [MaxLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")]
        public string PaymentMethod { get; set; } = "";

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int MemberShipsPlanId { get; set; }

        public MemberShipsPlan? MemberShipsPlan { get; set; }
    }
}
