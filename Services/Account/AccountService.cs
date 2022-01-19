using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using examedu.DTO.AccountDTO;
using ExamEdu.DB;
using ExamEdu.DB.Models;
using ExamEdu.DTO.PaginationDTO;
using ExamEdu.Helper;

namespace examedu.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public AccountService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        private string ConvertToUnsign(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        private  IEnumerable<AccountResponse> addAccountToTotalList(
        IEnumerable<Student> students, IEnumerable<AcademicDepartment> AcademicDepartments, IEnumerable<Teacher> teachers)
        {
            List<AccountResponse> totalAccount = new List<AccountResponse>();

            foreach (var item in students)
            {
                var itemToResponse = _mapper.Map<AccountResponse>(item);
                totalAccount.Add(itemToResponse);
            }

            foreach (var item in AcademicDepartments)
            {
                var itemToResponse = _mapper.Map<AccountResponse>(item);
                totalAccount.Add(itemToResponse);
            }

            foreach (var item in teachers)
            {
                var itemToResponse = _mapper.Map<AccountResponse>(item);
                totalAccount.Add(itemToResponse);
            }

            totalAccount.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            return totalAccount;
        }


        public Tuple<int, IEnumerable<AccountResponse>> GetAccountList(PaginationParameter paginationParameter)
        {
            string searchName = paginationParameter.SearchName;
            searchName = searchName.Replace(":*|", " ").Replace(":*", "");
            searchName = ConvertToUnsign(searchName);

            IEnumerable<Student> studentList = _dataContext.Students.ToList().Where(t
                 => t.DeactivatedAt == null && (t.Email.ToUpper().Contains(searchName.ToUpper())
                || t.Fullname.ToUpper().Contains(searchName.ToUpper())
                || ConvertToUnsign(t.Fullname).ToUpper().Contains(searchName.ToUpper())));
            IEnumerable<AcademicDepartment> AcademicDepartmentList = _dataContext.AcademicDepartments.ToList().Where(t
                 => t.DeactivatedAt == null && (t.Email.ToUpper().Contains(searchName.ToUpper())));
            IEnumerable<Teacher> teacherList = _dataContext.Teachers.ToList().Where(t
                 => t.DeactivatedAt == null && (t.Email.ToUpper().Contains(searchName.ToUpper())
                || t.Fullname.ToUpper().Contains(searchName.ToUpper())
                || ConvertToUnsign(t.Fullname).ToUpper().Contains(searchName.ToUpper())));

            IEnumerable<AccountResponse> totalAccount = addAccountToTotalList(studentList, AcademicDepartmentList, teacherList);

            return Tuple.Create(totalAccount.Count(), PaginationHelper.GetPage(totalAccount,
                paginationParameter.PageSize, paginationParameter.PageNumber));
        }
        public Tuple<int, IEnumerable<AccountResponse>> GetDeactivatedAccountList(PaginationParameter paginationParameter)
        {
            string searchName = paginationParameter.SearchName;
            searchName = searchName.Replace(":*|", " ").Replace(":*", "");
            searchName = ConvertToUnsign(searchName);

            IEnumerable<Student> studentList = _dataContext.Students.ToList().Where(t
                 => t.DeactivatedAt != null && (t.Email.ToUpper().Contains(searchName.ToUpper())
                || t.Fullname.ToUpper().Contains(searchName.ToUpper())
                || ConvertToUnsign(t.Fullname).ToUpper().Contains(searchName.ToUpper())));
            IEnumerable<AcademicDepartment> AcademicDepartmentList = _dataContext.AcademicDepartments.ToList().Where(t
                 => t.DeactivatedAt != null && (t.Email.ToUpper().Contains(searchName.ToUpper())));
            IEnumerable<Teacher> teacherList = _dataContext.Teachers.ToList().Where(t
                 => t.DeactivatedAt != null && (t.Email.ToUpper().Contains(searchName.ToUpper())
                || t.Fullname.ToUpper().Contains(searchName.ToUpper())
                || ConvertToUnsign(t.Fullname).ToUpper().Contains(searchName.ToUpper())));

            IEnumerable<AccountResponse> totalAccount = addAccountToTotalList(studentList, AcademicDepartmentList, teacherList);

            return Tuple.Create(totalAccount.Count(), PaginationHelper.GetPage(totalAccount,
                paginationParameter.PageSize, paginationParameter.PageNumber));
        }
    }
}