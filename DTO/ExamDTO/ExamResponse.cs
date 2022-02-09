using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace examedu.DTO.ExamDTO
{
    public class ExamResponse
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string Description { get; set; }
        public DateTime ExamDay { get; set; }
        public int DurationInMinute { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool isFinalExam { get; set; }
        public bool IsCancelled { get; set; } = false;
        public int ModuleId { get; set; }
        public int SupervisorId { get; set; }
    }
}