using BL.Interfaces;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IMemberShipsPlanService _memberShipsPlanService;

        public OrdersController(
            IOrderService orderService,
            IProductService productService,
            IMemberShipsPlanService memberShipsPlanService)
        {
            _orderService = orderService;
            _productService = productService;
            _memberShipsPlanService = memberShipsPlanService;
        }

        public async Task<IActionResult> List()
        {
            // Fetch orders
            var orders = await _orderService.GetAllOrdersAsync() ?? new List<OrderDTO>();

            // Fetch products and memberships for dropdowns (if needed for modal)
            ViewBag.Products = await _productService.GetAllProductsAsync() ?? new List<ProductDTO>();
            ViewBag.MemberShipsPlans = await _memberShipsPlanService.GetAllAsync() ?? new List<MemberShipsPlanDTO>();

            return View(orders);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _productService.GetAllProductsAsync() ?? new List<ProductDTO>();
            ViewBag.MemberShipsPlans = await _memberShipsPlanService.GetAllAsync() ?? new List<MemberShipsPlanDTO>();
            return View(new OrderDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO orderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateOrderAsync(orderDto);
                return RedirectToAction(nameof(List));
            }

            ViewBag.Products = await _productService.GetAllProductsAsync() ?? new List<ProductDTO>();
            ViewBag.MemberShipsPlans = await _memberShipsPlanService.GetAllAsync() ?? new List<MemberShipsPlanDTO>();
            return View(orderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderDTO orderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderService.UpdateOrderAsync(orderDto);
                return RedirectToAction(nameof(List));
            }

            return BadRequest("Invalid data.");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return RedirectToAction(nameof(List));
        }
    }

}
