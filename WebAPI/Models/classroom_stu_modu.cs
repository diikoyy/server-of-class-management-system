using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class classroom_stu_modu
    {
        public int Number { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; } 
    }
}
