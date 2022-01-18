using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class StudentAnswer
    {
        public int StudentAnswerId { get; set; }
        public string StudentAnswerContent { get; set; }
        
         public int ExamQuestionId { get; set; }
        public ExamQuestion ExamQuestion { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
        
    }
}