using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DTO.RefreshToken
{
    public class Token
    {
        public string RefreshToken { get; set; }
        public DateTime Expire { get; set; }
        public Token(string refreshToken, DateTime expire)
        {
            RefreshToken = refreshToken;
            Expire = expire;
        }
    }
}