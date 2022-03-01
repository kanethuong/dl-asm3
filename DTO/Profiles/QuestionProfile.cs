using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.DTO.ExamQuestionsDTO;
using BackEnd.DTO.QuestionDTO;
using examedu.DTO.QuestionDTO;
using ExamEdu.DB.Models;

namespace examedu.DTO.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionResponse>();
            CreateMap<FEQuestion, QuestionResponse>().ForMember(q => q.QuestionId, s => s.MapFrom(s => s.FEQuestionId));
            CreateMap<Answer, AnswerResponse>();
            CreateMap<FEAnswer, AnswerResponse>();
            CreateMap<Question, QuestionAnswerResponse>();
            CreateMap<FEQuestion, QuestionAnswerResponse>();
            CreateMap<Answer, AnswerContentResponse>();
            CreateMap<FEAnswer, AnswerContentResponse>();
            CreateMap<RequestAddQuestionInput, AddQuestionRequest>()
                    .ForMember(dest => dest.Questions, opt =>
                    {
                        opt.PreCondition(src => src.isFinalExam == false);
                        opt.MapFrom(src => src.Questions);
                    })
                    .ForMember(dest => dest.FEQuestions, opt =>
                    {
                        opt.PreCondition(src => src.isFinalExam == true);
                        opt.MapFrom(src => src.Questions);
                    })
                    .ForMember(dest => dest.Questions.Select(q => q.LevelId), opt => opt.MapFrom(src => src.LevelId))
                    .ForMember(dest => dest.Questions.Select(q => q.ModuleId), opt => opt.MapFrom(src => src.ModuleId));
            CreateMap<QuestionInput, Question>();
            CreateMap<QuestionInput, FEQuestion>();
            CreateMap<AnswerInput, Answer>();
            CreateMap<AnswerInput, FEAnswer>();
        }
    }
}