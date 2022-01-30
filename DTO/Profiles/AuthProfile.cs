using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.DTO.AuthDTO;
using examedu.DTO.AccountDTO;
using ExamEdu.DB.Models;

namespace BackEnd.DTO.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Student, AuthResponse>().ForMember(ac => ac.Role, act => act.Ignore())
                .ForMember(ac => ac.Role, a => a.MapFrom(s => s.Role.RoleName));
            CreateMap<Teacher, AuthResponse>().ForMember(ac => ac.Role, act => act.Ignore())
                .ForMember(ac => ac.Role, t => t.MapFrom(s => s.Role.RoleName));
            CreateMap<AcademicDepartment, AuthResponse>().ForMember(ac => ac.Role, act => act.Ignore())
                .ForMember(ac => ac.Role, t => t.MapFrom(s => s.Role.RoleName));
            CreateMap<Administrator, AuthResponse>().ForMember(ac => ac.Role, act => act.Ignore())
                .ForMember(ac => ac.Role, c => c.MapFrom(s => s.Role.RoleName));

            CreateMap<AccountResponse, AuthResponse>().ForMember(au => au.AccountId, s => s.MapFrom(ac=>ac.ID));
        }
    }
}