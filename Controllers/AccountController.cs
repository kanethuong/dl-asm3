using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using examedu.DTO.AccountDTO;
using examedu.Services.Account;
using ExamEdu.DTO;
using ExamEdu.DTO.PaginationDTO;
using Microsoft.AspNetCore.Mvc;

namespace examedu.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

       
        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the list of account in the db with pagination config
        /// </summary>
        /// <param name="paginationParameter">Pagination parameters from client</param>
        /// <returns>200: List of account with pagination / 404: search username not found</returns>
        [HttpGet("accountList")]
        public ActionResult<PaginationResponse<IEnumerable<AccountResponse>>> GetAccountList([FromQuery] PaginationParameter paginationParameter)
        {
            (int totalRecord, IEnumerable<AccountResponse> listAccount) =  _accountService.GetAccountList(paginationParameter);

            if (totalRecord == 0)
            {
                return NotFound(new ResponseDTO(404, "Search email not found"));
            }

            return Ok(new PaginationResponse<IEnumerable<AccountResponse>>(totalRecord, listAccount));
        }

        /// <summary>
        /// Get the list of account in the db with pagination config
        /// </summary>
        /// <param name="paginationParameter">Pagination parameters from client</param>
        /// <returns>200: List of account with pagination / 404: search username not found</returns>
        [HttpGet("deactivatedAccountList")]
        public ActionResult<PaginationResponse<IEnumerable<AccountResponse>>> GetDeactivatedAccountList([FromQuery] PaginationParameter paginationParameter)
        {
            (int totalRecord, IEnumerable<AccountResponse> listAccount) =  _accountService.GetDeactivatedAccountList(paginationParameter);

            if (totalRecord == 0)
            {
                return NotFound(new ResponseDTO(404, "Search email not found"));
            }

            return Ok(new PaginationResponse<IEnumerable<AccountResponse>>(totalRecord, listAccount));
        }
    }
}