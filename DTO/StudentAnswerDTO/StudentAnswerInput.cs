using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DTO.StudentAnswerDTO
{
    public class StudentAnswerInput
    {
        public string StudentAnswerContent { get; set; }
        public int StudentId { get; set; }
        public int ExamQuestionId { get; set; }
    }
}