using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class ClassDTO
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public DateTime Schdule { get; set; }
        public int Capacity { get; set; }
        public int TrainerId { get; set; }
        public TrainerDTO? Trainer { get; set; }
       // public ICollection<ApplicationUserDTO> Users { get; set; } = new List<ApplicationUserDTO>();
    }

}
