using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public bool isCorrect { get; set; }

        // Many answer - one question
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}