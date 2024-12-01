using Microsoft.AspNetCore.Mvc;
using BL.Interfaces;
using Domains.Dtos;
using System.IO;

namespace GymManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        // GET: Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return View(productDto);

            // Handle image upload
            if (productDto.Image != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(productDto.Image.FileName);
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await productDto.Image.CopyToAsync(stream);
                }

                productDto.ImagePath = $"/images/{fileName}";
            }
            else
            {
                // Retain existing image if no new image is uploaded
                productDto.ImagePath = productDto.ImagePath;
            }

            await _productService.AddProductAsync(productDto);
            return RedirectToAction("List");
        }

        // GET: Edit Modal Data
        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Json(product);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            // Handle image update
            if (productDto.Image != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(productDto.Image.FileName);
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await productDto.Image.CopyToAsync(stream);
                }
                productDto.ImagePath = $"/images/{fileName}";
            }

            await _productService.UpdateProductAsync(productDto);
            return RedirectToAction("List");
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return Json(new { success = false, message = "Product not found" });

            await _productService.DeleteProductAsync(id);
            return Json(new { success = true });
        }
    }
}
