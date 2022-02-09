using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DTO.StudentAnswerDTO
{
    public class StudentFEAnswerInput
    {
        public string StudentAnswerContent { get; set; }
        public int StudentId { get; set; }
        public int ExamFEQuestionId { get; set; }
    }
}