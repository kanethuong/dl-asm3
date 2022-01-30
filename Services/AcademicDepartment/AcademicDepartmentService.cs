using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamEdu.DB;
using ExamEdu.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
{
    public class AcademicDepartmentService : IAcademicDepartment
    {
        private readonly DataContext _dataContext;
        public AcademicDepartmentService(DataContext dataContext)
        {
            this._dataContext=dataContext;
        }

        public async Task<AcademicDepartment> GetAcademicDepartmentByEmail(string email)
        {
            return await _dataContext.AcademicDepartments.Where(s => s.Email.ToLower().Equals(email.ToLower()) && s.DeactivatedAt == null).FirstOrDefaultAsync();
        }
    }
}