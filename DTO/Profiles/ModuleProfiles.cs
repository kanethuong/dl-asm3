using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExamEdu.DB.Models;
using ExamEdu.DTO.ModuleDTO;

namespace ExamEdu.DTO.Profiles
{
    public class ModuleProfiles : Profile
    {
        public ModuleProfiles()
        {
            CreateMap<Module, ModuleResponse>();

        }
    }
}