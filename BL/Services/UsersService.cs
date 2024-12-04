using BL.Interfaces;
using Domains;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class UsersService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .Select(user => new ApplicationUserDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email!
                }).ToListAsync();
        }

        public async Task<ApplicationUserDTO?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return new ApplicationUserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!
            };
        }

        public async Task<bool> CreateUserAsync(ApplicationUserDTO userDto)
        {
            var user = new ApplicationUser
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                FullName = userDto.FullName
            };

            var result = await _userManager.CreateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserAsync(ApplicationUserDTO userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id);
            if (user == null) return false;

            user.FullName = userDto.FullName;
            user.Email = userDto.Email;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
