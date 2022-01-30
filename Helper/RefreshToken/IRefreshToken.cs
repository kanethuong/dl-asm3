using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Helper.RefreshToken
{
    public interface IRefreshToken
    {
        string GenerateRefreshToken(string email);
        string GetEmailByRefreshToken(string token);
        void RemoveRefreshTokenByEmail(string email);
    }
}