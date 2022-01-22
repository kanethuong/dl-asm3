using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using examedu.Services;
using ExamEdu.DB.Models;
using ExamEdu.DTO;
using ExamEdu.DTO.ExamDTO;
using ExamEdu.DTO.PaginationDTO;
using ExamEdu.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamEdu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;        
        public ExamController(IExamService examService,IMapper mapper,IStudentService studentService)
        {
            _examService = examService;
            _mapper = mapper;
            _studentService = studentService;
        }
        [HttpGet("{studentId:int}")]
        public async Task<IActionResult> GetExamScheduleByStudentId(int studentId,[FromQuery] PaginationParameter paginationParameter)
        {
            bool isStudentExist = _studentService.CheckStudentExist(studentId);
            if(isStudentExist==false){
                return NotFound(new ResponseDTO(404, "Student not found"));
            }
            (int totalRecord, List<Exam> examSchedules)= await _examService.getExamByStudentId(studentId,paginationParameter);
            
            if (totalRecord == 0)
            {
                return NotFound(new ResponseDTO(404, "Student doesn't have exam schedules"));
            }
            IEnumerable<ExamScheduleResponse> examScheduleResponses = _mapper.Map<IEnumerable<ExamScheduleResponse>>(examSchedules);
            return Ok(new PaginationResponse<IEnumerable<ExamScheduleResponse>>(totalRecord, examScheduleResponses));
        }
        
    }
}