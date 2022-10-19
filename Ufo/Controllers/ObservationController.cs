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
        private readonly InterfaceObservasjonRepository _db;

        public ObservationController(InterfaceObservasjonRepository db)
        {
            _db = db;
        }

        [HttpPost("addObservation")]
        public async Task<ActionResult> Lagre(Observasjon innObservasjon)
        {
            return Ok(await _db.Lagre(innObservasjon));
        }

        [HttpGet("fetchAllObservations")]
        public async Task<ActionResult> HentAlle()
        {
            List<Observasjon> alleObservasjoner = await _db.HentAlle();
            return Ok(alleObservasjoner);
        }

        [HttpGet("fetchOneObservation{id}")]
        public async Task<ActionResult> HentEn(int id)
        {
            Observasjon enObservasjon = await _db.HentEn(id);
            return Ok(enObservasjon);
        }

        [HttpPost("editObservation")]
        public async Task<ActionResult> Endre(Observasjon endreObservasjon)
        {
            return Ok(await _db.Endre(endreObservasjon));
        }

        [HttpDelete("deleteObservation{id}")]
        public async Task<ActionResult> Slett(int id)
        {
            return Ok(await _db.Slett(id));
        }
    }
}
