using Domains;
using Domains.Dtos.UserDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RolesController> _logger; // Logger for debugging

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger; // Initialize logger
        }

        // GET: ManageRoles
        public async Task<IActionResult> ManageRoles(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("User ID cannot be empty.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new ManageRolesViewModel
            {
                UserId = user.Id,
                UserRoles = userRoles.ToList(),
                Roles = roles.Select(role => new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name!,
                    IsAssigned = userRoles.Contains(role.Name!)
                }).ToList() // This should not be null now
            };

            return View(model);
        }

        // POST: ManageRoles
        [HttpPost]
        public async Task<IActionResult> ManageRoles(ManageRolesViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserId) || model.Roles == null)
            {
                return BadRequest("User ID or Roles cannot be null.");
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            // Get the selected roles from the model
            var selectedRoles = model.Roles.Where(r => r.IsAssigned).Select(r => r.Name).ToList();

            // Add new roles to the user
            var addResult = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!addResult.Succeeded)
            {
                foreach (var error in addResult.Errors)
                {
                    ModelState.AddModelError("", $"Failed to add role '{error.Description}'.");
                }
                return View(model);
            }

            // Remove roles from the user that were unselected
            var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!removeResult.Succeeded)
            {
                foreach (var error in removeResult.Errors)
                {
                    ModelState.AddModelError("", $"Failed to remove role '{error.Description}'.");
                }
                return View(model);
            }

            // If everything is successful, redirect to Users/Index after saving
            return RedirectToAction("Index", "Users");
        }

        // GET: CreateRole
        public IActionResult CreateRole()
        {
            var model = new ManageRolesViewModel
            {
                Roles = _roleManager.Roles.Select(role => new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name!
                }).ToList()
            };
            return View(model);
        }

        // POST: CreateRole
        [HttpPost]
        public async Task<IActionResult> CreateRole(ManageRolesViewModel model)
        {
            // Ensure the Roles list is initialized
            model.Roles ??= new List<RoleDto>();

            if (!string.IsNullOrWhiteSpace(model.NewRoleName))
            {
                var newRole = new IdentityRole(model.NewRoleName);
                var result = await _roleManager.CreateAsync(newRole);

                if (result.Succeeded)
                {
                    // Add the new role to the list to reflect it immediately
                    model.Roles.Add(new RoleDto
                    {
                        Id = newRole.Id,
                        Name = newRole.Name!
                    });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            // Reload existing roles
            model.Roles = await _roleManager.Roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Name = role.Name!
            }).ToListAsync();

            return View(model);
        }

        // GET: EditRole
        public async Task<IActionResult> EditRole(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Role ID cannot be empty.");
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var model = new RoleDto
            {
                Id = role.Id,
                Name = role.Name!
            };

            return View(model);
        }

        // POST: EditRole
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleDto model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    return NotFound("Role not found.");
                }

                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("CreateRole"); // Redirect to the role management page
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        // POST: DeleteRole
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Role ID cannot be empty.");
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("CreateRole"); // Redirect to the role management page
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction("CreateRole"); // Redirect even if there were errors
        }
    }
}
