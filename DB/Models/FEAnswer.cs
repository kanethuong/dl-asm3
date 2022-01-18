namespace ExamEdu.DB.Models
{
    public class FEAnswer
    {
        public int FEAnswerId { get; set; }
        public string AnswerContent { get; set; }
        public bool isCorrect { get; set; }

        // Many answer - one question
        public int FEQuestionId { get; set; }
        public FEQuestion FEQuestion { get; set; }
    }
}