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

        [HttpPost("logIn")]
        public async Task<ActionResult> LoggInn(User user)
        {
            if (ModelState.IsValid)
            {
                bool returnResult = await _db.Login(user);
                if (!returnResult)
                {
                    _log.LogInformation("Login failed for " + user.Username);
                    return Ok(false);
                }
                return Ok(true);
            }
            _log.LogInformation("Error in validation");
            return BadRequest("Wrong on input validation");
        }
    }
}
