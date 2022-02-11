using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamEdu.DB;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.ExamQuestions
{
    public class ExamQuestionsService : IExamQuestionsService
    {
        private readonly DataContext _dataContext;
        public ExamQuestionsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Get a list of question id by exam id and student id
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="studentId"></param>
        /// <param name="isFinalExam"></param>
        /// <returns></returns>
        public async Task<List<int>> GetListQuestionIdByExamIdAndStudentId(int examId, int studentId, bool isFinalExam)
        {
            if (isFinalExam)
            {
                return await (from e in _dataContext.Exam_FEQuestions
                              join std in _dataContext.StudentFEAnswers
                              on e.ExamFEQuestionId equals std.ExamFEQuestionId
                              where e.ExamId == examId && std.StudentId == studentId
                              select e.FEQuestionId).ToListAsync();
            }
            else
            {
                return await (from e in _dataContext.ExamQuestions
                              join std in _dataContext.StudentAnswers
                              on e.ExamQuestionId equals std.ExamQuestionId
                              where e.ExamId == examId && std.StudentId == studentId
                              select e.QuestionId).ToListAsync();
            }
        }
    }
}