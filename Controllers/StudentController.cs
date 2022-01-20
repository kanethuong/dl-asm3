using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using examedu.DTO.StudentDTO;
using examedu.Services;
using ExamEdu.DTO;
using Microsoft.AspNetCore.Mvc;

namespace examedu.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("markReport/{studentID:int}/{moduleID:int}")]
        public async Task<ActionResult<List<ModuleMarkDTO>>> DeactivateAccount(int studentId, int moduleId)
        {
            List<ModuleMarkDTO> listResult = await _studentService.getModuleMark(studentId, moduleId);
            if(listResult == null)
            {
                return BadRequest(new ResponseDTO(400, "StudentID or ModuleID not exist"));
            }
            if(listResult.Count == 0)
            {
                return NotFound(new ResponseDTO(404, "Student maynot have exam in this module"));
            }
            return Ok(listResult);
        }
    }
}