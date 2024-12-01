using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.MemberShipsPlan)
                .ToListAsync();

            return customers.Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                SecondName = c.SecondName,
                DateOfBirth = c.DateOfBirth,
                Phone = c.Phone,
                Address = c.Address,
                JoinDate = c.JoinDate,
                MemberShipsPlanId = c.MemberShipsPlanId,
                MemberShipsPlan = c.MemberShipsPlan != null
                    ? new MemberShipsPlanDTO
                    {
                        MemberShipsPlanId = c.MemberShipsPlan.MemberShipsPlanId,
                        MemberShipPlanName = c.MemberShipsPlan.MemberShipPlanName,
                        Durations = c.MemberShipsPlan.Durations,
                        Price = c.MemberShipsPlan.Price
                    }
                    : null
            }).ToList();
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.MemberShipsPlan)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null) return null;

            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                SecondName = customer.SecondName,
                DateOfBirth = customer.DateOfBirth,
                Phone = customer.Phone,
                Address = customer.Address,
                JoinDate = customer.JoinDate,
                MemberShipsPlanId = customer.MemberShipsPlanId,
                MemberShipsPlan = customer.MemberShipsPlan != null
                    ? new MemberShipsPlanDTO
                    {
                        MemberShipsPlanId = customer.MemberShipsPlan.MemberShipsPlanId,
                        MemberShipPlanName = customer.MemberShipsPlan.MemberShipPlanName,
                        Durations = customer.MemberShipsPlan.Durations,
                        Price = customer.MemberShipsPlan.Price
                    }
                    : null
            };
        }

        public async Task CreateCustomerAsync(CustomerDTO customerDto)
        {
            var customer = new Customer
            {
                FirstName = customerDto.FirstName,
                SecondName = customerDto.SecondName,
                DateOfBirth = customerDto.DateOfBirth,
                Phone = customerDto.Phone,
                Address = customerDto.Address,
                JoinDate = customerDto.JoinDate,
                MemberShipsPlanId = customerDto.MemberShipsPlanId
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(CustomerDTO customerDto)
        {
            var customer = await _context.Customers.FindAsync(customerDto.CustomerId);

            if (customer == null)
                throw new Exception("Customer not found");

            // Update fields only if they have changed
            customer.FirstName = customerDto.FirstName ?? customer.FirstName;
            customer.SecondName = customerDto.SecondName ?? customer.SecondName;
            customer.DateOfBirth = customerDto.DateOfBirth != default
                ? customerDto.DateOfBirth
                : customer.DateOfBirth;
            customer.Phone = customerDto.Phone ?? customer.Phone;
            customer.Address = customerDto.Address ?? customer.Address;
            customer.JoinDate = customerDto.JoinDate != default
                ? customerDto.JoinDate
                : customer.JoinDate;
            customer.MemberShipsPlanId = customerDto.MemberShipsPlanId;

            _context.Customers.Update(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the customer. Please try again later.", ex);
            }
        }

        public async Task<List<MemberShipsPlanDTO>> GetAllMemberShipsPlansAsync()
        {
            var plans = await _context.MemberShipsPlans.ToListAsync();

            return plans.Select(p => new MemberShipsPlanDTO
            {
                MemberShipsPlanId = p.MemberShipsPlanId,
                MemberShipPlanName = p.MemberShipPlanName,
                Durations = p.Durations,
                Price = p.Price
            }).ToList();
        }

        // Implementing DeleteCustomerAsync
        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
