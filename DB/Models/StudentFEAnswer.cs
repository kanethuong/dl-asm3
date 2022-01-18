namespace ExamEdu.DB.Models
{
    public class StudentFEAnswer
    {
        public int StudentFEAnswerId { get; set; }
        public string StudentAnswerContent { get; set; }
        
        public int ExamFEQuestionId { get; set; }
        public Exam_FEQuestion Exam_FEQuestion { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}