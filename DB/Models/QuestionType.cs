using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class QuestionType
    {
        public int QuestionTypeId { get; set; }
        public string QuestionTypeName { get; set; }
        //One Question Type - many question
        public ICollection<Question> Questions { get; set; }
        public ICollection<FEQuestion> FEQuestions { get; set; }

    }
}