using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamEdu.DB.Models;

namespace examedu.DTO.QuestionDTO
{
    public class QuestionResponse
    {
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; }
        public string QuestionImageURL { get; set; }
        public DateTime ApprovedAt { get; set; }
        public int QuestionTypeID { get; set; }

        public List<Answer> Answers { get; set; }
    }
}