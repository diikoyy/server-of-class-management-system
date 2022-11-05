using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class classroom_module
    {
        public int Number { get; set; }
        public int ModuleID { get; set; }

        public string ModuleName { get; set; }
        public string ExamDate { get; set; }
    }
}
