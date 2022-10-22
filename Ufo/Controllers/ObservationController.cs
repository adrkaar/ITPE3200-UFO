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

        public ObservationController(InterfaceObservationRepository db, InterfaceCommentRepository dbComments)
        {
            _db = db;
        }

        [HttpPost("addObservation")]
        public async Task<ActionResult> Lagre(Observation innObservasjon)
        {
            return Ok(await _db.SaveObservation(innObservasjon));
        }

        [HttpGet("fetchAllObservations")]
        public async Task<ActionResult> HentAlle()
        {
            List<Observation> alleObservasjoner = await _db.FetchAllObservations();
            return Ok(alleObservasjoner);
        }

        [HttpGet("fetchOneObservation/{id}")]
        public async Task<ActionResult> HentEn(int id)
        {
            Observation enObservasjon = await _db.GetOneObservation(id);
            return Ok(enObservasjon);
        }

        [HttpPost("editObservation")]
        public async Task<ActionResult> Endre(Observation endreObservasjon)
        {
            return Ok(await _db.ChangeObservation(endreObservasjon));
        }

        [HttpDelete("deleteObservation/{id}")]
        public async Task<ActionResult> Slett(int id)
        {
            return Ok(await _db.DeleteObservation(id));
        }
    }
}
