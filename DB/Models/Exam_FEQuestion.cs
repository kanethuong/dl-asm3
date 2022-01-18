using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class Exam_FEQuestion
    {

        public int ExamFEQuestionId { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int FEQuestionId { get; set; }
        public FEQuestion FEQuestion { get; set; }
        
        public int ExamCode { get; set; }

        public float QuestionMark { get; set; }

        // Many-Many StudentAnswer
        public ICollection<StudentFEAnswer> StudentFEAnswers { get; set; }
    }
}