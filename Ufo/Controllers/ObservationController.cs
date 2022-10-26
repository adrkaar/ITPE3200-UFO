using Microsoft.AspNetCore.Mvc;
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

        public ObservationController(InterfaceObservationRepository db)
        {
            _db = db;
        }

        [HttpPost("addObservation")]
        public async Task<ActionResult> SaveObservation(Observation inObservation)
        {
            return Ok(await _db.SaveObservation(inObservation));
        }

        [HttpGet("fetchAllObservations")]
        public async Task<ActionResult> FetchAllObservations()
        {
            List<Observation> allObservatoins = await _db.FetchAllObservations();
            return Ok(allObservatoins);
        }

        [HttpGet("fetchOneObservation/{id}")]
        public async Task<ActionResult> GetOneObservation(int id)
        {
            Observation oneObservation = await _db.GetOneObservation(id);
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
            return Ok(await _db.DeleteObservation(id));
        }

        [HttpGet("fetchUfoTypes")]
        public async Task<ActionResult> FetchUfoTypes()
        {
            List<UfoType> ufotypes = await _db.FetchUfoTypes();
            return Ok(ufotypes);
        }
    }
}
