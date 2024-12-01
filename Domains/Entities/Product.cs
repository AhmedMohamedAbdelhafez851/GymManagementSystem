using System;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [MaxLength(50, ErrorMessage = "Product code cannot exceed 50 characters.")]
        public string? ProductCode { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; } = "";

        [MaxLength(250, ErrorMessage = "Image path cannot exceed 250 characters.")]
        public string? ImagePath { get; set; }

        public ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
