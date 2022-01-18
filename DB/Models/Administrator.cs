using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class Administrator
    {
        public int AdministratorId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        

        // Many ad - one role
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}