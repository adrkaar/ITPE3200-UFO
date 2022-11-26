using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ufo.DAL;
using Ufo.Models;

namespace Ufo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObservationController : ControllerBase
    {
        private readonly InterfaceObservationRepository _db;
        private ILogger<ObservationController> _log;

        public ObservationController(InterfaceObservationRepository db, ILogger<ObservationController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost("addObservation")]
        public async Task<ActionResult> SaveObservation(Observation inObservation)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(UserController._loggedIn))) { return Unauthorized("Not logged in"); }
            if (ModelState.IsValid)
            {
                bool returnOk = await _db.SaveObservation(inObservation);
                if (!returnOk)
                {
                    _log.LogInformation("Observation was not saved");
                    return BadRequest("Observation was not saved");
                }
                return Ok(returnOk);
            }
            else
            {
                _log.LogInformation("Error in input validation");
                return BadRequest("Error in input validation");
            }
        }

        [HttpGet("fetchAllObservations")]
        public async Task<ActionResult> FetchAllObservations()
        {
            List<Observation> allObservatoins = await _db.FetchAllObservations();
            if (allObservatoins == null)
            {
                _log.LogInformation("Table in database is empty");
                return NotFound("Table in database is empty");
            }
            return Ok(allObservatoins);
        }

        [HttpGet("fetchOneObservation/{id}")]
        public async Task<ActionResult> GetOneObservation(int id)
        {
            Observation oneObservation = await _db.GetOneObservation(id);
            if (oneObservation == null)
            {
                _log.LogInformation("Observation was not found");
                return NotFound("Observation was not found");
            }
            return Ok(oneObservation);
        }

        [HttpPut("editObservation")]
        public async Task<ActionResult> ChangeObservation(Observation changeObservation)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(UserController._loggedIn))) { return Unauthorized("Not logged in"); }
            if (ModelState.IsValid)
            {
                bool returnOk = await _db.ChangeObservation(changeObservation);
                if (!returnOk)
                {
                    _log.LogInformation("Observation could not be changed");
                    return NotFound("Observation could not be changed");
                }
                return Ok(returnOk);
            }
            else
            {
                _log.LogInformation("Error in input validation");
                return BadRequest("Error in input validation");
            }
        }

        [HttpDelete("deleteObservation/{id}")]
        public async Task<ActionResult> DeleteObservation(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(UserController._loggedIn))) { return Unauthorized("Not logged in"); }
            bool deleteOk = await _db.DeleteObservation(id);
            if (!deleteOk)
            {
                _log.LogInformation("Observation could not be deleted");
                return NotFound("Observation could not be deleted");
            }
            return Ok(deleteOk);
        }

        [HttpGet("fetchUfoTypes")]
        public async Task<ActionResult> FetchUfoTypes()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedIn))) { return Unauthorized("Not logged in"); }
            List<UfoType> ufotypes = await _db.FetchUfoTypes();
            if (ufotypes == null)
            {
                _log.LogInformation("Table in database is empty");
                return NotFound("Table in database is empty");
            }
            return Ok(ufotypes);
        }

        [HttpGet("fetchAllLocations")]
        public async Task<ActionResult> FetchAllLocations()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggedIn))) { return Unauthorized("Not logged in"); }
            List<Observation> allLocations = await _db.FetchAllLocations();
            if (allLocations == null)
            {
                _log.LogInformation("Table in database is empty");
                return NotFound("Table in database is empty");
            }
            return Ok(allLocations);
        }
    }
}