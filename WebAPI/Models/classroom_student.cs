using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class classroom_student
    {
        public int Number { get; set; }
        public int StudentID { get; set; }

        public string StudentName { get; set; }
        public string Major { get; set; }
        public int Intake { get; set; }
    }
}
