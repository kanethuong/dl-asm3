using AutoMapper;
using BackEnd.DTO.ClassDTO;
using BackEnd.DTO.ClassModuleDTO;
using ExamEdu.DB.Models;

namespace BackEnd.DTO.Profiles
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<ModuleTeacherStudentInput, ClassModule>();
            CreateMap<CreateClassInput, Class>()
                .ForMember(dest => dest.ClassModules,opt=> opt.MapFrom(src=> src.ModuleTeacherIds))

                ;
        }
    }
}