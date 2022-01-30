using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DTO.Email
{
    public class EmailContent
    {
        [Required]
        public string ToEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public bool IsBodyHtml { get; set; }
        [Required]
        public string Body { get; set; }
    }
}