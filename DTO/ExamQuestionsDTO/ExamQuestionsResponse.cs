using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DTO.ExamQuestionsDTO
{
    public class ExamQuestionsResponse
    {
        public string QuestionContent { get; set; }
        public string QuestionImageURL { get; set; }
        public List<AnswerContentResponse> Answers { get; set; }
    }
}