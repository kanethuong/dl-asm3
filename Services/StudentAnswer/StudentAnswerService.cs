using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamEdu.DB;
using ExamEdu.DB.Models;

namespace ExamEdu.Services
{
    public class StudentAnswerService : IStudentAnswerService
    {
        private readonly DataContext _dataContext;
        public StudentAnswerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<int> InsertStudentAnswers(List<StudentAnswer> answers)
        {
            foreach (var item in answers)
            {
                var checkStudent = _dataContext.Students.Any(t => t.StudentId == item.StudentId && t.DeactivatedAt == null);
                if (checkStudent is false)
                {
                    return -1;
                }
                var checkExamQuestion = _dataContext.ExamQuestions.Any(e => e.ExamQuestionId == item.ExamQuestionId);
                if (checkExamQuestion is false)
                {
                    return -2;
                }
            }
            _dataContext.StudentAnswers.AddRange(answers);
            int rowInserted = await _dataContext.SaveChangesAsync();
            return rowInserted;

        }
        public async Task<int> InsertFEStudentAnswers(List<StudentFEAnswer> answers)
        {
            foreach (var item in answers)
            {
                var checkStudent = _dataContext.Students.Any(t => t.StudentId == item.StudentId && t.DeactivatedAt == null);
                if (checkStudent is false)
                {
                    return -1;
                }
                var checkExamQuestion = _dataContext.Exam_FEQuestions.Any(e => e.ExamFEQuestionId == item.ExamFEQuestionId);
                if (checkExamQuestion is false)
                {
                    return -2;
                }
            }
            _dataContext.StudentFEAnswers.AddRange(answers);
            int rowInserted = await _dataContext.SaveChangesAsync();
            return rowInserted;

        }
    }
}