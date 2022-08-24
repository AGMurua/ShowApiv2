using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
using ShowApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShowApi.Managers
{
    public class AuthManager
    {
        private readonly IConfiguration _config;
        private readonly UserRepository _context;

        public AuthManager(IConfiguration config, UserRepository context)
        {
            _config = config;
            _context = context;
            _context.Table = "users";
        }
        public BaseResponse<object> LogIn(string user, string password)
        {
            var userEntity = _context.FindByUserName(user.ToLower());
            if (userEntity == null)
                return new BaseResponse<object>(_config.GetValue<string>("Response:Auth:BadUser:Code"),
                    _config.GetValue<string>("Response:Auth:BadUser:Message"));
            else if (userEntity.Password != GetHashString(password))
                return new BaseResponse<object>(_config.GetValue<string>("Response:Auth:BadPass:Code"),
                    _config.GetValue<string>("Response:Auth:BadPass:Message")); var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user", userEntity.UserName),
                    new Claim("profile", userEntity.Role),
                    new Claim("userId", userEntity.UserId)
                });


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_config.GetSection("JwtKey").ToString());
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = new BaseResponse<object>();
            result.Data =tokenHandler.WriteToken(token);
            return result;
        }

        public object Register(string user, string password)
        {
            var payload = new UserEntity
            {
                UserName = user.ToLower(),
                Password = GetHashString(password),
                Role = "user"
            };
            return _context.Save(payload);
        }
        public static byte[] GetHash(string password)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static string GetHashString(string password)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(password))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

    }
}
