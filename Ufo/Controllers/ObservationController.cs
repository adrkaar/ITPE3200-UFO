using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ufo.Models;
using Ufo.DAL;

namespace Ufo.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    public class ObservationController : ControllerBase
    {
        private readonly InterfaceObservasjonRepository _db;

        public ObservationController(InterfaceObservasjonRepository db)
        {
            _db = db;
        }

        /*
        public async Task<List<Observasjon>> HentAlle()                                                 // Med async +
        {
            try
            {
                List<Observasjon> alleObservasjoner = await _db.Observasjoner.ToListAsync();            // Hente Tabellen "Observasjon"
                return alleObservasjoner;
            }
            catch
            {
                return null;
            }
        }
        */

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
        public async Task<ActionResult> HentEn(int id)                                  // Hente en Observasjon paa ID
        {
            Observasjon enObservasjon = await _db.HentEn(id);
            return Ok(enObservasjon);
        }

        [HttpPost("editObservation")]
        public async Task<ActionResult> Endre(Observasjon endreObservasjon)                // Gi mulighet til aa endre alle linjer i en Observasjon
        {
            return Ok(await _db.Endre(endreObservasjon));
        }

        [HttpDelete("deleteObservation{id}")]
        public async Task<ActionResult> Slett(int id)                                       // Slett en Observasjon paa ID
        {
            return Ok(await _db.Slett(id));
        }
    }
}
