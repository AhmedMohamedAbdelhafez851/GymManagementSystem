using Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IMemberShipsPlanService
    {
        Task<IEnumerable<MemberShipsPlanDTO>> GetAllAsync();
        Task<MemberShipsPlanDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MemberShipsPlanDTO dto);
        Task<bool> UpdateAsync(MemberShipsPlanDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
