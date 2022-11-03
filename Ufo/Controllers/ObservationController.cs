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
            bool returnOk = await _db.SaveObservation(inObservation);
            if (!returnOk)
            {
                _log.LogInformation("Observation was not saved");
                return BadRequest("Observation was not saved");
            }
            return Ok(returnOk);
        }

        [HttpGet("fetchAllObservations")]
        public async Task<ActionResult> FetchAllObservations()
        {
            List<Observation> allObservatoins = await _db.FetchAllObservations();
            if (allObservatoins == null)
            {
                _log.LogInformation("Table in database is empty");
                return BadRequest("Table in database is empty");
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
                return BadRequest("Observation was not found");
            }
            return Ok(oneObservation);
        }

        [HttpPost("editObservation")]
        public async Task<ActionResult> ChangeObservation(Observation changeObservation)
        {
            bool returnOk = await _db.ChangeObservation(changeObservation);
            if (!returnOk)
            {
                _log.LogInformation("Observation could not be changed");
                return BadRequest("Observation could not be changed");
            }
            return Ok(returnOk);
        }

        [HttpDelete("deleteObservation/{id}")]
        public async Task<ActionResult> DeleteObservation(int id)
        {
            bool deleteOk = await _db.DeleteObservation(id);
            if (!deleteOk)
            {
                _log.LogInformation("Observation could not be deleted");
                return BadRequest("Observation could not be deleted");
            }
            return Ok(deleteOk);
        }

        [HttpGet("fetchUfoTypes")]
        public async Task<ActionResult> FetchUfoTypes()
        {
            List<UfoType> ufotypes = await _db.FetchUfoTypes();
            if (ufotypes == null)
            {
                _log.LogInformation("Table in database is empty");
                return BadRequest("Table in database is empty");
            }
            return Ok(ufotypes);
        }

        [HttpGet("fetchAllLocations")]
        public async Task<ActionResult> FetchAllLocations()
        {
            List<Observation> allLocations = await _db.FetchAllLocations();
            if (allLocations == null)
            {
                _log.LogInformation("Table in database is empty");
                return BadRequest("Table in database is empty");
            }
            return Ok(allLocations);
        }
    }
}
