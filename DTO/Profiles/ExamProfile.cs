using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExamEdu.DB.Models;
using ExamEdu.DTO.ExamDTO;

namespace ExamEdu.DTO.Profiles
{
    public class ExamProfile:Profile
    {
        public ExamProfile()
        {
            CreateMap<Exam, ExamScheduleResponse>();
        }
        
    }
}