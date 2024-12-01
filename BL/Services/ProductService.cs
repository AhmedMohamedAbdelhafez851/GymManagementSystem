using BL.Interfaces;
using BL.Data;
using Microsoft.EntityFrameworkCore;
using Domains.Entities;
using Domains.Dtos;

namespace BL.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products.Select(MapToDto);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? null! : MapToDto(product);
        }

        public async Task<ProductDTO> AddProductAsync(ProductDTO productDto)
        {
            var product = MapToEntity(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Map the added product back to DTO and return it
            return MapToDto(product);
        }


        public async Task UpdateProductAsync(ProductDTO ProductDTO)
        {
            var product = MapToEntity(ProductDTO);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        // Manual Mapping: Entity -> DTO
        private ProductDTO MapToDto(Product product)
        {
            return new ProductDTO
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Price = product.Price,
                ProductCode = product.ProductCode,
                Description = product.Description,
                ImagePath = product.ImagePath // Ensure this is returned correctly
            };
        }


        // Manual Mapping: DTO -> Entity
        private Product MapToEntity(ProductDTO ProductDTO)
        {
            return new Product
            {
                ProductId = ProductDTO.ProductId,
                Title = ProductDTO.Title,
                Price = ProductDTO.Price,
                ProductCode = ProductDTO.ProductCode,
                Description = ProductDTO.Description!,
                ImagePath = ProductDTO.ImagePath // Make sure this is correctly passed and mapped
            };
        }

    }
}
