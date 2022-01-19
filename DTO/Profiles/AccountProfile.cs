using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using examedu.DTO.AccountDTO;
using ExamEdu.DB.Models;

namespace examedu.DTO.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Student, AccountResponse>().ForMember(ac => ac.ID, s => s.MapFrom(st => st.StudentId));
            CreateMap<AcademicDepartment, AccountResponse>().ForMember(ac => ac.ID, s => s.MapFrom(st => st.AcademicDepartmentId));
            CreateMap<Teacher, AccountResponse>().ForMember(ac => ac.ID, s => s.MapFrom(st => st.TeacherId));
        }
    }
}