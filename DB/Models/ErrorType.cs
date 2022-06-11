using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class ErrorType
    {
        public int ErrorTypeId { get; set; }
        public int ErrorTypeName { get; set; }
        public ICollection<StudentError> StudentErrors { get; set; }
    }
}