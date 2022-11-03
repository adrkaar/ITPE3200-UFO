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
            return Ok("Observation was saved");
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
            return Ok(await _db.ChangeObservation(changeObservation));
        }

        [HttpDelete("deleteObservation/{id}")]
        public async Task<ActionResult> DeleteObservation(int id)
        {
            bool deleteOk = await _db.DeleteObservation(id);
            if (!deleteOk)
            {
                _log.LogInformation("Observation was not deleted");
                return BadRequest("Observation was not deleted");
            }
            return Ok("Observation was deleted");
        }

        [HttpGet("fetchUfoTypes")]
        public async Task<ActionResult> FetchUfoTypes()
        {
            List<UfoType> ufotypes = await _db.FetchUfoTypes();
            return Ok(ufotypes);
        }

        [HttpGet("fetchAllLocations")]
        public async Task<ActionResult> FetchAllLocations()
        {
            List<Observation> allLocations = await _db.FetchAllLocations();
            return Ok(allLocations);
        }
    }
}
