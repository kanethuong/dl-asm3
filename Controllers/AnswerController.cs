using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExamEdu.DB.Models;
using ExamEdu.DTO;
using ExamEdu.DTO.StudentAnswerDTO;
using ExamEdu.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamEdu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IStudentAnswerService _studentAnswerService;
        private readonly IMapper _mapper;
        public AnswerController(IStudentAnswerService studentAnswerService, IMapper mapper)
        {
            _studentAnswerService = studentAnswerService;
            _mapper = mapper;
        }
        [HttpPost("PT")]
        public async Task<IActionResult> SubmitAnswer(List<StudentAnswerInput> answerInputs)
        {
            var answers = _mapper.Map<List<StudentAnswer>>(answerInputs);
            var rs = await _studentAnswerService.InsertStudentAnswers(answers);
            if (rs == -1)
            {
                return BadRequest(new ResponseDTO(400, "Student is not found"));
            }
            else if (rs == -2)
            {
                return BadRequest(new ResponseDTO(400, "Exam question is not found"));
            }
            return Created("", new ResponseDTO(201, "Submit answer successful"));
        }
        [HttpPost("FE")]
        public async Task<IActionResult> SubmitFEAnswer(List<StudentFEAnswerInput> answerInputs)
        {
            var answers = _mapper.Map<List<StudentFEAnswer>>(answerInputs);
            var rs = await _studentAnswerService.InsertFEStudentAnswers(answers);
            if (rs == -1)
            {
                return BadRequest(new ResponseDTO(400, "Student is not found"));
            }
            else if (rs == -2)
            {
                return BadRequest(new ResponseDTO(400, "Exam FE question is not found"));
            }
            return Created("", new ResponseDTO(201, "Submit final exam answer successful"));
        }
    }
}