using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using ExamEdu.DB;
using ExamEdu.DB.Models;
using ExamEdu.DTO.PaginationDTO;
using ExamEdu.Helper;
using Microsoft.EntityFrameworkCore;

namespace ExamEdu.Services
{
    public class ModuleService : IModuleService
    {
        private readonly DataContext _db;
        public ModuleService(DataContext db)
        {
            _db = db;
        }
        public async Task<Tuple<int, IEnumerable<Module>>> getAllModuleStudentHaveExam(int studentId, PaginationParameter paginationParameter)
        {
            //Select all the exam student have attend
            var allExamOfStudent = await _db.StudentExamInfos.Where(e => e.StudentId == studentId).Select(e => e.ExamId).ToListAsync();
            if (allExamOfStudent.Count() == 0)
            {
                return null;
            }
            //Select all module Id in the exam student have attend by joining Exam table and module table
            var moduleList = await _db.Exams.Join(_db.Modules,
                                             e => e.ModuleId,
                                             m => m.ModuleId,
                                            (e, m) => new
                                            {
                                                ExamId = e.ExamId,
                                                ModuleId = m.ModuleId,
                                                ModuleName = m.ModuleName
                                            })
                                        .Where(e => allExamOfStudent.Contains(e.ExamId))
                                        .Select(m => new Module
                                        {
                                            ModuleId = m.ModuleId,
                                            ModuleName = m.ModuleName
                                        })
                                        .Distinct().ToListAsync();
            return Tuple.Create(moduleList.Count, moduleList.GetPage(paginationParameter));

        }

        public async Task<Module> getModuleByID(int id)
        {
            return await _db.Modules.Where(m => m.ModuleId == id).FirstOrDefaultAsync();
        }


        public async Task<Tuple<int, IEnumerable<Module>>> getModules(PaginationParameter paginationParameter)
        {
            string searchName = paginationParameter.SearchName;
            searchName = searchName.Replace(":*|", " ").Replace(":*", "");
            searchName = ConvertToUnsign(searchName);

            var moduleList = await _db.Modules.Where(m => m.ModuleName.ToUpper().Contains(searchName.ToUpper()) || m.ModuleCode.ToUpper().Contains(searchName.ToUpper())).ToListAsync();
            return Tuple.Create(moduleList.Count, moduleList.GetPage(paginationParameter));
        }

        private string ConvertToUnsign(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}