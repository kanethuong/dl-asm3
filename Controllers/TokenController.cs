using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.DTO.AccessTokenDTO;
using BackEnd.Helper.Authentication;
using BackEnd.Helper.RefreshToken;
using examedu.Services.Account;
using ExamEdu.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IRefreshToken _refreshToken;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public TokenController(IRefreshToken refreshToken, IJwtGenerator jwtGenerator, IAccountService accountService, IMapper mapper)
        {
            _refreshToken = refreshToken;
            _jwtGenerator = jwtGenerator;
            _accountService = accountService;
            _mapper = mapper;
        }

        /// <summary>
        /// Refresh the access token using the refresh token in cookies
        /// </summary>
        /// <returns>200: New accessToken / 
        /// 400: Not found refresh token in client cookies, 
        /// token is expired, 
        /// payload in access token and refresh token not the same</returns>
        [HttpPost("refresh")]
        public async Task<ActionResult<Token>> RefreshAccessToken()
        {
            try
            {
                //Get the access token from header
                var _bearerToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                //Get payload data from access token
                var principal = _jwtGenerator.GetPrincipalFromExpiredToken(_bearerToken);

                //Get email and role claim in payload
                var claims = principal.Identities.First().Claims.ToList();
                var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var role = claims?.FirstOrDefault(c => c.Type.EndsWith("role", StringComparison.CurrentCultureIgnoreCase))?.Value;

                //Check whether email with that role exist in db
                var account = await _accountService.GetAccountByEmail(email);
                string roleName=await _accountService.GetRoleName(account.Item1.RoleID);
                if (account == null || !roleName.Equals(role, StringComparison.CurrentCultureIgnoreCase))
                {
                    return BadRequest(new ResponseDTO(400, "Invalid access token"));
                }

                //Get refresh token from cookies
                if (!Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken))
                {
                    return BadRequest(new ResponseDTO(400, "Invalid token request"));
                }

                //Validation refresh token, check expire and get email from refresh token
                var tokenMail = _refreshToken.GetEmailByRefreshToken(refreshToken);
                if (tokenMail == null)
                {
                    return BadRequest(new ResponseDTO(400, "Invalid client request"));
                }

                //Generate new access token
                var newAccessToken = _jwtGenerator.GenerateAccessToken(principal.Claims);
                return Ok(new Token { AccessToken = newAccessToken });
            }
            catch
            {
                return BadRequest(new ResponseDTO(400, "Invalid access token"));
            }
        }
    }
}