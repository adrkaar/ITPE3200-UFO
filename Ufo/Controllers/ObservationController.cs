using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Observation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observation.Controllers
{
    [Route("[controller]/[action]")]
    public class ObservationController : ControllerBase
    {
        private readonly ObservasjonContext _db;
        public ObservationController(ObservasjonContext db)
        {
            _db = db;
        }
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

        public async Task<bool> Lagre(Observasjon innObservasjon)
        {
            try
            {
                _db.Observasjoner.Add(innObservasjon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Observasjon> HentEn(int id)                                  // Hente en Observasjon paa ID
        {
            try
            {
                Observasjon enObservasjon = await _db.Observasjoner.FindAsync(id);
                return enObservasjon;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Endre(Observasjon endreObservasjon)                // Gi mulighet til aa endre alle linjer i en Observasjon
        {
            try
            {
                Observasjon enObservasjon = await _db.Observasjoner.FindAsync(endreObservasjon.Id);
                enObservasjon.Navn = endreObservasjon.Navn;
                enObservasjon.Dato = endreObservasjon.Dato;
                enObservasjon.Tid = endreObservasjon.Tid;
                enObservasjon.Beskrivelse = endreObservasjon.Beskrivelse;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Slett(int id)                                       // Slett en Observasjon paa ID
        {
            try
            {
                Observasjon enObservasjon = await _db.Observasjoner.FindAsync(id);
                _db.Observasjoner.Remove(enObservasjon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
