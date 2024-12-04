using Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUserDTO>> GetAllUsersAsync();
        Task<ApplicationUserDTO?> GetUserByIdAsync(string id);
        Task<bool> CreateUserAsync(ApplicationUserDTO userDto);
        Task<bool> UpdateUserAsync(ApplicationUserDTO userDto);
        Task<bool> DeleteUserAsync(string id);
    }
}
