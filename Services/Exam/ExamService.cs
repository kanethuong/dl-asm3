using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamEdu.DB;
using ExamEdu.DB.Models;
using ExamEdu.DTO.PaginationDTO;
using ExamEdu.Helper;
using Microsoft.EntityFrameworkCore;

namespace ExamEdu.Services
{
    public class ExamService:IExamService
    {
        private readonly DataContext _db;

        public ExamService(DataContext dataContext)
        {
            _db = dataContext;
        }
        /// <summary>
        /// Get Exam Schedule of student, which have Exam day > now
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="paginationParameter"></param>
        /// <returns></returns>
        public async Task<Tuple<int, IEnumerable<Exam>>> getExamByStudentId(int studentId, PaginationParameter paginationParameter)
        {
            var allExamOfStudent = await _db.StudentExamInfos.Where(e => e.StudentId == studentId).Select(e => e.ExamId).ToListAsync();
            if (allExamOfStudent.Count() == 0)
            {
                return null;
            }
            List<Exam> examList = new List<Exam>();
            foreach (var examId in allExamOfStudent)
            {
                var exam =await _db.Exams.FirstOrDefaultAsync(e => e.ExamId == examId && e.ExamDay >= DateTime.Now);
                if(exam is not null){
                    examList.Add(exam);
                }
            }
            
            return Tuple.Create(examList.Count, examList.GetPage(paginationParameter)) ;
        }
    }
}