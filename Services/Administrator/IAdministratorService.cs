using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamEdu.DB.Models;

namespace BackEnd.Services
{
    public interface IAdministratorService
    {
        Task<Administrator> GetAdministratorByEmail(string email);
    }
}