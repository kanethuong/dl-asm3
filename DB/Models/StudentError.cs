using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class StudentError
    {
        public int StudentErrorId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime Time { get; set; }
        public string Comment { get; set; }
        public bool IsComfirmed { get; set; }=false;
        public int ErrorTypeId { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}