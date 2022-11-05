using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class classroom_attendance
    {
        public int Number { get; set; }
        public int StudentID { get; set; }

        public int ModuleID { get; set; }
        public string DateOfAttendance { get; set; }
    }
}
