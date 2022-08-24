using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShowApi.Data.Entities;
using ShowApi.Data.Repositories;
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
        public object LogIn(string user, string password)
        {
            var userEntity = _context.FindByUserName(user);
            if (userEntity == null)
                return null;
            else if (userEntity.Password != GetHashString(password))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_config.GetSection("JwtKey").ToString());
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user),
                    new Claim(ClaimTypes.Role, "admin")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

        public object Register(string user, string password)
        {
            var payload = new UserEntity
            {
                UserName = user,
                Password = GetHashString(password)
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
