using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExamEdu.DB.Models;
using ExamEdu.DTO.StudentAnswerDTO;

namespace ExamEdu.DTO.Profiles
{
    public class StudentAnswerProfile :Profile
    {
        public StudentAnswerProfile()
        {
            CreateMap<StudentAnswerInput,StudentAnswer>();
            CreateMap<StudentFEAnswerInput,StudentFEAnswer>();
        }
    }
}