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
using Microsoft.EntityFrameworkCore;

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

        private IEnumerable<AccountResponse> addAccountToTotalList(
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

        /// <summary>
        /// get all account in all role exepct admin
        /// </summary>
        /// <param name="paginationParameter"></param>
        /// <returns></returns>
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

        /// <summary>
        /// get all DeactivatedAccount in all role except admin
        /// </summary>
        /// <param name="paginationParameter"></param>
        /// <returns></returns>
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

        // private void sendEmail(string email, string password)
        // {
        //     EmailContent emailContent = new EmailContent();
        //     emailContent.IsBodyHtml = true;
        //     emailContent.ToEmail = email;
        //     emailContent.Subject = "[EXAMEDU] Your password";
        //     emailContent.Body = password;
        //     _emailService.SendEmailAsync(emailContent);
        // }

        private string processPasswordAndSendEmail(string email)
        {
            string password = AutoGeneratorPassword.passwordGenerator(15, 5, 5, 5);

            //sendEmail(email, password);
            password = BCrypt.Net.BCrypt.HashPassword(password);

            return password;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountInput"></param>
        /// <returns>1 if sucess / 0 if fail(duplicate email) / -1 all other fail (invalid role...)</returns>
        public async Task<int> InsertNewAccount(AccountInput accountInput)
        {
            PaginationParameter paginationParameter = new PaginationParameter { PageNumber = 1, PageSize = 1, SearchName = accountInput.Email };
            if (GetAccountList(paginationParameter).Item1 == 1 || GetDeactivatedAccountList(paginationParameter).Item1 == 1)
            {
                return 0;
            }

            switch (accountInput.RoleID)
            {
                //CASE NAY CHI DUNG KHI MUON TAO TAI KHOAN ADMIN KO BO CMT CASE NAY
                // case 0:
                //     var adminToAdd = _mapper.Map<Administrator>(accountInput);
                //     adminToAdd.RoleId = accountInput.RoleID;
                //     adminToAdd.Password = processPasswordAndSendEmail(adminToAdd.Email);
                //     _dataContext.Administrators.Add(adminToAdd);
                //     if(await _dataContext.SaveChangesAsync() !=1)
                //     {
                //         return -1;
                //     }
                //     else
                //     {
                //         return 1;
                //     }
                case 1:
                    var studentToAdd = _mapper.Map<Student>(accountInput);
                    studentToAdd.RoleId = accountInput.RoleID;
                    studentToAdd.Password = processPasswordAndSendEmail(studentToAdd.Email);
                    _dataContext.Students.Add(studentToAdd);
                    if (await _dataContext.SaveChangesAsync() != 1)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                case 2:
                    var teacherToAdd = _mapper.Map<Teacher>(accountInput);
                    teacherToAdd.RoleId = accountInput.RoleID;
                    teacherToAdd.Password = processPasswordAndSendEmail(teacherToAdd.Email);
                    _dataContext.Teachers.Add(teacherToAdd);
                    if (await _dataContext.SaveChangesAsync() != 1)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                case 3:
                    var academicDepartToAdd = _mapper.Map<AcademicDepartment>(accountInput);
                    academicDepartToAdd.RoleId = accountInput.RoleID;
                    academicDepartToAdd.Password = processPasswordAndSendEmail(academicDepartToAdd.Email);
                    _dataContext.AcademicDepartments.Add(academicDepartToAdd);
                    if (await _dataContext.SaveChangesAsync() != 1)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Deactivate base on role
        /// </summary>
        /// <param name="id">id of user</param>
        /// <param name="role">role of user</param>
        /// <returns>-1:Already Deactivated / 0:fail / 1:success</returns>
        public async Task<int> DeactivateAccount(int id, int roleID)
        {
            switch (roleID)
            {
                case 1:
                    var studentToDeActivate = await _dataContext.Students.Where(s =>
                         s.DeactivatedAt == null && s.StudentId == id).FirstOrDefaultAsync();
                    if (studentToDeActivate == null)
                    {
                        return -1;
                    }
                    studentToDeActivate.DeactivatedAt = DateTime.Now;
                    return await _dataContext.SaveChangesAsync();
                case 2:
                    var teacherToDeActivate = await _dataContext.Teachers.Where(s =>
                         s.DeactivatedAt == null && s.TeacherId == id).FirstOrDefaultAsync();
                    if (teacherToDeActivate == null)
                    {
                        return -1;
                    }
                    teacherToDeActivate.DeactivatedAt = DateTime.Now;
                    return await _dataContext.SaveChangesAsync();
                case 3:
                    var AcademicDepartToDeActivate = await _dataContext.AcademicDepartments.Where(s =>
                         s.DeactivatedAt == null && s.AcademicDepartmentId == id).FirstOrDefaultAsync();
                    if (AcademicDepartToDeActivate == null)
                    {
                        return -1;
                    }
                    AcademicDepartToDeActivate.DeactivatedAt = DateTime.Now;
                    return await _dataContext.SaveChangesAsync();
                default:
                    return 0;
            }
        }
    }
}