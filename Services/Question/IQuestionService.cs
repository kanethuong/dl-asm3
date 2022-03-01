using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.DTO.ExamQuestionsDTO;
using BackEnd.DTO.QuestionDTO;
using examedu.DTO.QuestionDTO;
using ExamEdu.DB.Models;

namespace examedu.Services
{
    public interface IQuestionService
    {
        Task<List<QuestionResponse>> getQuestionByModuleLevel(int moduleID, int levelID, bool isFinalExam);
        Task<List<QuestionAnswerResponse>> GetListQuestionAnswerByListQuestionId(List<int> questionIdList, int examId, int examCode, bool isFinalExam);
        Task<int> InsertNewQuestionRequestInfor(AddQuestionRequest addQuestionRequest);
        Task<int> InsertNewQuestionsAndAnswers(List<QuestionInput> questions, int addQuestionRequestId, bool isFinalExam);
    }
}