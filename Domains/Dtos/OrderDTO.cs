using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public ProductDTO? Product { get; set; }
        public int MemberShipsPlanId { get; set; }
        public MemberShipsPlanDTO? MemberShipsPlan { get; set; }
    }

}
