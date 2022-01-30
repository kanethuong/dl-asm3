using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BackEnd.DTO.RefreshToken;

namespace BackEnd.Helper.RefreshToken
{
    public class RefreshToken : IRefreshToken
    {
        private Dictionary<string, Token> _refreshTokenList;
        public RefreshToken()
        {
            _refreshTokenList = new Dictionary<string, Token>();
        }

        public string GenerateRefreshToken(string email)
        {
            removeExpiredRefreshToken();

            //Generate random string
            string refreshToken;
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }

            //Set expire time for refresh token to 2 weeks
            DateTime expire = DateTime.Now.AddDays(14);

            //Create token
            Token token = new Token(refreshToken, expire);

            //Replace if token exist
            if (_refreshTokenList.ContainsKey(email))
            {
                _refreshTokenList.Remove(email);
            }
            _refreshTokenList.Add(email, token);
            return refreshToken;
        }

        /// <summary>
        /// Remove all the expired refresh token from list
        /// </summary>
        private void removeExpiredRefreshToken()
        {
            foreach (var entry in _refreshTokenList)
            {
                if (DateTime.Compare(entry.Value.Expire, DateTime.Now) < 0)
                {
                    _refreshTokenList.Remove(entry.Key);
                }
            }
        }

        /// <summary>
        /// Get payload data (email) in refresh token
        /// </summary>
        /// <param name="token">Refresh token</param>
        /// <returns>email / null (not found token)</returns>
        public string GetEmailByRefreshToken(string token)
        {
            foreach (var entry in _refreshTokenList)
            {
                if (entry.Value.RefreshToken == token)
                {
                    if (DateTime.Compare(entry.Value.Expire, DateTime.Now) < 0)
                    {
                        _refreshTokenList.Remove(entry.Key);
                        return null;
                    }
                    return entry.Key;
                }
            }
            return null;
        }

        /// <summary>
        /// Remove refresh token using email
        /// </summary>
        /// <param name="email">email contain token</param>
        public void RemoveRefreshTokenByEmail(string email)
        {
            _refreshTokenList.Remove(email);
        }
    }
}