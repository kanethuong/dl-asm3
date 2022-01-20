using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using examedu.DTO.StudentDTO;

namespace examedu.Services
{
    public interface IStudentService
    {
        Task<List<ModuleMarkDTO>> getModuleMark(int studentID, int moduleID);
    }
}