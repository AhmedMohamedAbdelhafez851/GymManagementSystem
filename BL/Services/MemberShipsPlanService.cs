using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public class MemberShipsPlanService : IMemberShipsPlanService
    {
        private readonly ApplicationDbContext _context;

        public MemberShipsPlanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberShipsPlanDTO>> GetAllAsync()
        {
            return await _context.MemberShipsPlans
                .Select(p => new MemberShipsPlanDTO
                {
                    MemberShipsPlanId = p.MemberShipsPlanId,
                    MemberShipPlanName = p.MemberShipPlanName,
                    Durations = p.Durations,
                    Price = p.Price
                })
                .ToListAsync();
        }

        public async Task<MemberShipsPlanDTO?> GetByIdAsync(int id)
        {
            var plan = await _context.MemberShipsPlans.FindAsync(id);
            if (plan == null) return null;

            return new MemberShipsPlanDTO
            {
                MemberShipsPlanId = plan.MemberShipsPlanId,
                MemberShipPlanName = plan.MemberShipPlanName,
                Durations = plan.Durations,
                Price = plan.Price
            };
        }

        public async Task<bool> CreateAsync(MemberShipsPlanDTO dto)
        {
            var plan = new MemberShipsPlan
            {
                MemberShipPlanName = dto.MemberShipPlanName,
                Durations = dto.Durations,
                Price = dto.Price
            };

            _context.MemberShipsPlans.Add(plan);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(MemberShipsPlanDTO dto)
        {
            var plan = await _context.MemberShipsPlans.FindAsync(dto.MemberShipsPlanId);
            if (plan == null) return false;

            plan.MemberShipPlanName = dto.MemberShipPlanName;
            plan.Durations = dto.Durations;
            plan.Price = dto.Price;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plan = await _context.MemberShipsPlans.FindAsync(id);
            if (plan == null) return false;

            _context.MemberShipsPlans.Remove(plan);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
