using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .Include(o => o.MemberShipsPlan)
                .ToListAsync();

            return orders.Select(o => MapToDTO(o)).ToList();
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Product)
                .Include(o => o.MemberShipsPlan)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            return order == null ? null! : MapToDTO(order);
        }

        public async Task CreateOrderAsync(OrderDTO orderDto)
        {
            var order = MapToEntity(orderDto);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(OrderDTO orderDto)
        {
            var order = MapToEntity(orderDto);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        // Manual mapping methods
        private OrderDTO MapToDTO(Order order)
        {
            return new OrderDTO
            {
                OrderId = order.OrderId,
                Amount = order.Amount,
                OrderDate = order.OrderDate,
                PaymentMethod = order.PaymentMethod,
                ProductId = order.ProductId,
                Product = order.Product == null ? null : new ProductDTO
                {
                    ProductId = order.Product.ProductId,
                    Title = order.Product.Title
                },
                MemberShipsPlanId = order.MemberShipsPlanId,
                MemberShipsPlan = order.MemberShipsPlan == null ? null : new MemberShipsPlanDTO
                {
                    MemberShipsPlanId = order.MemberShipsPlan.MemberShipsPlanId,
                    MemberShipPlanName = order.MemberShipsPlan.MemberShipPlanName
                }
            };
        }

        private Order MapToEntity(OrderDTO dto)
        {
            return new Order
            {
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                OrderDate = dto.OrderDate,
                PaymentMethod = dto.PaymentMethod,
                ProductId = dto.ProductId,
                MemberShipsPlanId = dto.MemberShipsPlanId
            };
        }
    }
}
