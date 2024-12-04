using BL.Interfaces;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GymManagementSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customers/List
        public async Task<IActionResult> List()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            var membershipPlans = await _customerService.GetAllMemberShipsPlansAsync();
            ViewBag.MemberShipsPlans = membershipPlans; // Pass membership plans to View
            return View(customers);
        }

        // GET: Customers/Create
        public async Task<IActionResult> Create()
        {
            var membershipPlans = await _customerService.GetAllMemberShipsPlansAsync();
            ViewBag.MemberShipsPlans = membershipPlans;
            return View(new CustomerDTO());
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                var membershipPlans = await _customerService.GetAllMemberShipsPlansAsync();
                ViewBag.MemberShipsPlans = membershipPlans;
                return View(customerDto);
            }

            await _customerService.CreateCustomerAsync(customerDto);
            return RedirectToAction(nameof(List));
        }

        // GET: Customers/EditModal/{id}
        [HttpGet]
        public async Task<IActionResult> EditModal(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Json(new
            {
                customer.CustomerId,
                customer.FirstName,
                customer.SecondName,
                DateOfBirth = customer.DateOfBirth.ToString("yyyy-MM-dd"),
                customer.Phone,
                customer.Address,
                JoinDate = customer.JoinDate.ToString("yyyy-MM-dd"),
                customer.MemberShipsPlanId
            });
        }

        // POST: Customers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] CustomerDTO customerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Invalid model state" });
                }

                await _customerService.UpdateCustomerAsync(customerDto);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the error
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Customers/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction(nameof(List));
        }

       

        
    }
}