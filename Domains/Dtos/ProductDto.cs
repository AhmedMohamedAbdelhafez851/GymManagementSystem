using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Add this line

namespace Domains.Dtos
{

    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ProductCode { get; set; }
        public string? Description { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImagePath { get; set; }
    }

}

