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
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Auth([BindRequired]string user, [BindRequired]string password)
        {
            return Ok(_manager.LogIn(user, password));
        }

        [HttpPut]
        [AllowAnonymous]
        public ActionResult SignUp(string password, string userName)
        {
            return Ok(_manager.Register(userName, password));
        }
    }
}
