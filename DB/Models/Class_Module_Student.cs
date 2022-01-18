using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class Class_Module_Student
    {
        public int ClassModuleId { get; set; }
        public ClassModule ClassModule { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}