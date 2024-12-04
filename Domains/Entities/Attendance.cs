using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = "";
        public DateTime AttendanceDate { get; set; }
    }
}
