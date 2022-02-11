using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.DTO.ExamQuestionsDTO;
using BackEnd.Services.ExamQuestions;
using examedu.Services;
using ExamEdu.DTO;
using ExamEdu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ExamQuestionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExamQuestionsService _examQuestionsService;
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly IQuestionService _questionService;
        public ExamQuestionsController(IMapper mapper,
                                       IExamQuestionsService examQuestionsService,
                                       IStudentService studentService,
                                       IExamService examService,
                                       IQuestionService questionService)
        {
            _mapper = mapper;
            _examQuestionsService = examQuestionsService;
            _studentService = studentService;
            _examService = examService;
            _questionService = questionService;
        }

        /// <summary>
        /// Get the exam questions of an exam
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        [HttpGet("{studentId:int}/{examId:int}")]
        public async Task<ActionResult<List<ExamQuestionsResponse>>> GetExamQuestions(int studentId, int examId)
        {
            bool isStudentExist = _studentService.CheckStudentExist(studentId);
            if (!isStudentExist)
            {
                return NotFound(new ResponseDTO(404, "Student cannot be found!"));
            }

            var exam = await _examService.getExamById(examId);
            if (exam == null)
            {
                return NotFound(new ResponseDTO(404, "Exam cannot be found!"));
            }

            bool isFinalExam = _examService.IsFinalExam(examId);
            List<int> questIdList = new List<int>();
            questIdList = await _examQuestionsService.GetListQuestionIdByExamIdAndStudentId(examId, studentId, isFinalExam);
            if (questIdList.Count <= 0)
            {
                return NotFound(new ResponseDTO(404, "This exam does not have any question."));
            }
            List<ExamQuestionsResponse> examQuestList = new List<ExamQuestionsResponse>();
            examQuestList = await _questionService.GetListExamQuestionsByListQuestionId(questIdList, isFinalExam);
            return Ok(examQuestList);
        }
    }
}