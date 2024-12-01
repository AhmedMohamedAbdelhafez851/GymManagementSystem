using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class MemberShipsPlanDTO
    {
        public int MemberShipsPlanId { get; set; }
        public string MemberShipPlanName { get; set; } = string.Empty;
        public string Durations { get; set; } = string.Empty;
        public int Price { get; set; }
    }

}
