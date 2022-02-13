using System;
namespace ExamEdu.DTO.ExamDTO
{
    public class ProgressExamResponse
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDay { get; set; }
        public int DurationInMinute { get; set; }
        public bool IsCancelled { get; set; }
    }
}