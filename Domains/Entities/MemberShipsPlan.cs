using System;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class MemberShipsPlan
    {
        public int MemberShipsPlanId { get; set; }

        [Required(ErrorMessage = "Membership plan name is required.")]
        [MaxLength(100, ErrorMessage = "Membership plan name cannot exceed 100 characters.")]
        public string MemberShipPlanName { get; set; } = "";

        [Required(ErrorMessage = "Duration is required.")]
        [MaxLength(50, ErrorMessage = "Duration cannot exceed 50 characters.")]
        public string Durations { get; set; } = "";

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }

       // public int CustomerId { get; set; }

        public ICollection<Customer>? Customers { get; set; } = new List<Customer>();

      //  public Customer? Customer { get; set; }


        public ICollection<Order>? Orders { get; set; } = new List<Order>();

    }
}
