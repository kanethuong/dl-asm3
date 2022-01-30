using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DTO.Email
{
    public class EmailConfig
    {
        public string MailAddress { get; set; }
        public string MailPassword { get; set; }
        public int MailPort { get; set; }
    }
}