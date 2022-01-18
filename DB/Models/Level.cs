using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class Level
    {
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        // 1 Level - many question
        public ICollection<Question> Questions { get; set; }
        public ICollection<FEQuestion> FEQuestions { get; set; }
        
    }
}