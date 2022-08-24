using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShowApi.Managers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShowApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AuthManager _manager;

        public AuthenticateController(IConfiguration config, AuthManager manager)
        {
            _config = config;
            _manager = manager;
        }
        [HttpPost("LogIn")]
        [AllowAnonymous]
        public ActionResult Auth([BindRequired]string user, [BindRequired]string password)
        {
            var result = _manager.LogIn(user, password);
            if (result.Code == "401")
                return Unauthorized(result);
            return Ok(result.Data);
        }

        [HttpPut("SignUp")]
        [AllowAnonymous]
        public ActionResult SignUp([BindRequired] string userName, [BindRequired] string password)
        {
            return Ok(_manager.Register(userName, password));
        }
    }
}
