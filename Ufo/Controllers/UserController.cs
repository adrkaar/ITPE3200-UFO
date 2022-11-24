using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Ufo.DAL;
using Ufo.Models;

namespace Ufo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private InterfaceUserRepository _db;
        private ILogger<UserController> _log;

        private const string _loggedIn = "loggedIn";
        private const string _notLoggedIn = "";

        public UserController(InterfaceUserRepository db, ILogger<UserController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost("logIn")]
        public async Task<ActionResult> LogIn(User user)
        {
            if (ModelState.IsValid)
            {
                bool returnResult = await _db.LogIn(user);
                if (!returnResult)
                {
                    _log.LogInformation("Login failed");
                    HttpContext.Session.SetString(_loggedIn, _notLoggedIn);
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggedIn, _loggedIn);
                return Ok(true);
            }
            _log.LogInformation("Error in validation");
            return BadRequest("Error in input validation");
        }

        public void LogOut()
        {
            HttpContext.Session.SetString(_loggedIn, _notLoggedIn);
        }
    }
}
