using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace examedu.DTO.AccountDTO
{
    public class AccountResponse
    {
        public int ID { get; set; }
        public string Fullname { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}