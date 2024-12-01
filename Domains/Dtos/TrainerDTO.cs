using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class TrainerDTO
    {
        public int TrainerId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
    }

}
