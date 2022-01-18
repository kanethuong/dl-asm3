using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<Administrator> Administrators { get; set; }
        public ICollection<AcademicDepartment> AcademicDepartments { get; set; }
        public ICollection<Student> Students{ get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}