using Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO?> GetCustomerByIdAsync(int id);
        Task CreateCustomerAsync(CustomerDTO customerDto);
        Task UpdateCustomerAsync(CustomerDTO customerDto);
        Task<List<MemberShipsPlanDTO>> GetAllMemberShipsPlansAsync();
        Task DeleteCustomerAsync(int customerId);  // New method for deleting a customer
    }
}
