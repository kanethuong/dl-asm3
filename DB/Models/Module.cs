using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamEdu.DB.Models
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // 1 Module - many question
        public ICollection<Question> Questions { get; set; }
        public ICollection<FEQuestion> FEQuestions { get; set; }

        // Many-Many module
        public ICollection<Class> Classes { get; set; }
        public ICollection<ClassModule> ClassModules { get; set; }

        public ICollection<Exam> Exams { get; set; }
    }
}