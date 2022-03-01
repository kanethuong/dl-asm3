using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.DTO.ExamQuestionsDTO;
using BackEnd.DTO.QuestionDTO;
using examedu.DTO.QuestionDTO;
using ExamEdu.DB;
using ExamEdu.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace examedu.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public QuestionService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// get Question by module, level, and isFinalExam
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="levelID"></param>
        /// <param name="isFinalExam"></param>
        /// <returns></returns>
        public async Task<List<QuestionResponse>> getQuestionByModuleLevel(int moduleID, int levelID, bool isFinalExam)
        {
            List<QuestionResponse> listResponse = new List<QuestionResponse>();
            if (isFinalExam)
            {
                var QuestionListFromDB = await _dataContext.FEQuestions.Where(q =>
                    q.ModuleId == moduleID && q.LevelId == levelID && q.ApproveAt != null).ToListAsync();
                foreach (var question in QuestionListFromDB)
                {
                    //return await _db.Answers.Where(a => a.QuestionId == questionID).ToListAsync();
                    QuestionResponse questionResponse = _mapper.Map<QuestionResponse>(question);
                    questionResponse.Answers = new List<AnswerResponse>();
                    var Answers = await _dataContext.FEAnswers.Where(a => a.FEQuestionId == question.FEQuestionId).ToListAsync();
                    foreach (var answer in Answers)
                    {
                        questionResponse.Answers.Add(_mapper.Map<AnswerResponse>(answer));
                    }
                    listResponse.Add(questionResponse);
                }
            }
            else
            {
                var QuestionListFromDB = await _dataContext.Questions.Where(q =>
                    q.ModuleId == moduleID && q.LevelId == levelID && q.ApproveAt != null).ToListAsync();
                foreach (var question in QuestionListFromDB)
                {
                    QuestionResponse questionResponse = _mapper.Map<QuestionResponse>(question);
                    var Answers = await _dataContext.Answers.Where(a => a.QuestionId == question.QuestionId).ToListAsync();
                    foreach (var answer in Answers)
                    {
                        questionResponse.Answers.Add(_mapper.Map<AnswerResponse>(answer));
                    }
                    listResponse.Add(questionResponse);
                }
            }
            return listResponse;
        }

        /// <summary>
        /// Get a list of questions and answers by a list of question id
        /// </summary>
        /// <param name="questionIdList">list of question id</param>
        /// <param name="examId">use to get ExamQuestionId</param>
        /// <param name="examCode">use to get ExamQuestionId</param>
        /// <param name="isFinalExam"></param>
        /// <returns></returns>
        public async Task<List<QuestionAnswerResponse>> GetListQuestionAnswerByListQuestionId(List<int> questionIdList, int examId, int examCode, bool isFinalExam)
        {
            List<QuestionAnswerResponse> questionAnswerList = new List<QuestionAnswerResponse>();
            if (isFinalExam)
            {
                foreach (var questionId in questionIdList)
                {
                    FEQuestion question = await _dataContext.FEQuestions.Where(q => q.FEQuestionId == questionId && q.ApproveAt != null).FirstOrDefaultAsync();
                    var questionAnswerResponse = _mapper.Map<QuestionAnswerResponse>(question);
                    List<FEAnswer> answerList = await _dataContext.FEAnswers.Where(ans => ans.FEAnswerId == question.FEQuestionId).ToListAsync();
                    foreach (var answer in answerList)
                    {
                        questionAnswerResponse.Answers.Add(_mapper.Map<AnswerContentResponse>(answer));
                    }
                    questionAnswerResponse.ExamQuestionId = await _dataContext.Exam_FEQuestions.Where(e => e.ExamId == examId && e.ExamCode == examCode && e.FEQuestionId == questionId).Select(e => e.ExamFEQuestionId).FirstOrDefaultAsync();
                    questionAnswerList.Add(questionAnswerResponse);
                }
            }
            else
            {
                foreach (int questionId in questionIdList)
                {
                    Question question = await _dataContext.Questions.Where(q => q.QuestionId == questionId && q.ApproveAt != null).FirstOrDefaultAsync();
                    var questionAnswerResponse = _mapper.Map<QuestionAnswerResponse>(question);
                    List<Answer> answerList = await _dataContext.Answers.Where(ans => ans.QuestionId == question.QuestionId).ToListAsync();
                    foreach (var answer in answerList)
                    {
                        questionAnswerResponse.Answers.Add(_mapper.Map<AnswerContentResponse>(answer));
                    }
                    questionAnswerResponse.ExamQuestionId = await _dataContext.ExamQuestions.Where(e => e.ExamId == examId && e.ExamCode == examCode && e.QuestionId == questionId).Select(e => e.ExamQuestionId).FirstOrDefaultAsync();
                    questionAnswerList.Add(questionAnswerResponse);
                }
            }
            return questionAnswerList;
        }

        public async Task<int> InsertNewQuestionRequestInfor(AddQuestionRequest addQuestionRequest)
        {
            using (var dbContextTransaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    int rowInserted = 0;
                    _dataContext.AddQuestionRequests.Add(addQuestionRequest);
                    rowInserted = await _dataContext.SaveChangesAsync();
                    return rowInserted;
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<int> InsertNewQuestionsAndAnswers(List<QuestionInput> questions, int addQuestionRequestId, bool isFinalExam)
        {
            int rowInserted = 0;
            if (isFinalExam == false)
            {
                foreach (var question in questions)
                {
                    using (var dbContextTransaction = _dataContext.Database.BeginTransaction())
                    {
                        try
                        {
                            Question ptQuestion = _mapper.Map<Question>(question);
                            ptQuestion.AddQuestionRequestId = addQuestionRequestId;
                            _dataContext.Questions.Add(ptQuestion);
                            rowInserted = await _dataContext.SaveChangesAsync();

                            if (rowInserted != 0)
                            {
                                foreach (var ptAnswer in question.Answers)
                                {
                                    Answer answer = _mapper.Map<Answer>(ptAnswer);
                                    answer.QuestionId = ptQuestion.QuestionId;
                                    _dataContext.Answers.Add(answer);
                                    await _dataContext.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                return rowInserted;
                            }
                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            return 0;
                        }
                    }
                }
            }
            else
            {
                foreach (var question in questions)
                {
                    using (var dbContextTransaction = _dataContext.Database.BeginTransaction())
                    {
                        try
                        {
                            FEQuestion feQuestion = _mapper.Map<FEQuestion>(question);
                            feQuestion.AddQuestionRequestId = addQuestionRequestId;
                            _dataContext.FEQuestions.Add(feQuestion);
                            rowInserted = await _dataContext.SaveChangesAsync();

                            if (rowInserted != 0)
                            {
                                foreach (var feAnswer in question.Answers)
                                {
                                    FEAnswer answer = _mapper.Map<FEAnswer>(feAnswer);
                                    answer.FEQuestionId = feQuestion.FEQuestionId;
                                    _dataContext.FEAnswers.Add(answer);
                                    await _dataContext.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                return rowInserted;
                            }
                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            return 0;
                        }
                    }
                }
            }
            return rowInserted;
        }
    }
}