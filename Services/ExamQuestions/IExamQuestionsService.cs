using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.ExamQuestions
{
    public interface IExamQuestionsService
    {
        Task<List<int>> GetListQuestionIdByExamIdAndStudentId(int examId, int studentId, bool isFinalExam);
    }
}