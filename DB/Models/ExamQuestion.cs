using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class ExamQuestion
    {

        public int ExamQuestionId { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
        
        public int ExamCode { get; set; }

        public float QuestionMark { get; set; }

        // Many-Many StudentAnswer
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}