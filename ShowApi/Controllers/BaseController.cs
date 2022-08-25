using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ShowApi.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        internal bool checkProfile()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var test = identity.FindFirst("profile").Value;
                if (test != "admin")
                    return false;
                else return true;
            }
            return false;
        }

        internal string profile()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userId = identity.FindFirst("profile").Value;
                return userId;
            }
            return null;
        }

        internal string userId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userId = identity.FindFirst("userId").Value;
                return userId;
            }
            return null;
        }

        internal string userName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userName = identity.FindFirst("user").Value;
                return userName;
            }
            return null;
        }
    }
}
