using BL.Interfaces;
using Domains.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    public class MemberShipsPlanController : Controller
    {
        private readonly IMemberShipsPlanService _service;

        public MemberShipsPlanController(IMemberShipsPlanService service)
        {
            _service = service;
        }

        // List all membership plans
        public async Task<IActionResult> List()
        {
            var plans = await _service.GetAllAsync();
            return View(plans);
        }

        // Action for creating a new membership plan
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberShipsPlanDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(List));
        }

        // Action for editing a membership plan
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _service.GetByIdAsync(id);
            if (plan == null) return NotFound();

            // Return the view for editing, populated with the plan's data
            return View(plan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberShipsPlanDTO dto)
        {
            if (!ModelState.IsValid)
            {
                // If the form data is invalid, return to the Edit view with the model data
                return View(dto);
            }

            // Update the membership plan in the database
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(List));  // Redirect back to the list after saving
        }

        // Action for deleting a membership plan
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(List));  // Redirect back to the list after deletion
        }
    }
}
