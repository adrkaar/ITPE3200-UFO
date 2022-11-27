using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Ufo.DAL;
using Ufo.Models;

namespace Ufo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private ILogger<ContactController> _log;

        public ContactController(ILogger<ContactController> log)
        {
            _log = log;
        }

        [HttpPost("handleContact")]
        public async Task<ActionResult> handleContact(ContactMessage contactMessage)
        {
            if (contactMessage == null)
            {
                _log.LogInformation("Error");
                return BadRequest("Error");
            }
            else
            {
                _log.LogInformation(contactMessage.Name + contactMessage.Email + contactMessage.Message);

                
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("webapp2022ufo@gmail.com", "Sommer2022!dag123"),
                    EnableSsl = true,
                };

                smtpClient.Send("webapp2022ufo@gmail.com", contactMessage.Email, "UFO sightings", "Thanks for contacting us");
                
            }
            return Ok();
        }
    }
}
