using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace examedu.DTO.QuestionDTO
{
    public class AnswerResponse
    {
        public string AnswerContent { get; set; }
        public bool isCorrect { get; set; }
    }
}