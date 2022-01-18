using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class ClassModule
    {
        public int ClassModuleId { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Class_Module_Student> Class_Module_Students { get; set; }

        


    }
}