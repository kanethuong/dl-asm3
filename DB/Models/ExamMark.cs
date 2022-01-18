using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class ExamMark
    {
        
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; }
        
        public float? Mark { get; set; }
    }
}