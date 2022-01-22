using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamEdu.DB.Models;
using ExamEdu.DTO.PaginationDTO;

namespace ExamEdu.Services
{
    public interface IExamService
    {
        Task<Tuple<int, List<Exam>>> getExamByStudentId(int studentId, PaginationParameter paginationParameter);
    }
}