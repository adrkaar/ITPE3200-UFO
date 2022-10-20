using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public class ObservasjonRepository : InterfaceObservasjonRepository
    {
        private readonly ObservasjonContext _db;

        public ObservasjonRepository(ObservasjonContext db)
        {
            _db = db;
        }

        public async Task<bool> Lagre(Observasjon innObservasjon)
        {
            try
            {
                var nyObservasjonRad = new Observasjoner();

                nyObservasjonRad.Navn = innObservasjon.Navn;
                nyObservasjonRad.Dato = innObservasjon.Dato;
                nyObservasjonRad.Tid = innObservasjon.Tid;
                nyObservasjonRad.Beskrivelse = innObservasjon.Beskrivelse;
                nyObservasjonRad.Lokasjon = innObservasjon.Lokasjon;

                _db.Observasjoner.Add(nyObservasjonRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<List<Observasjon>> HentAlle()
        {
            try
            {
                List<Observasjon> alleObservasjoner = await _db.Observasjoner.Select(obs => new Observasjon
                {
                    Id = obs.Id,
                    Navn = obs.Navn,
                    Dato = obs.Dato,
                    Tid = obs.Tid,
                    Beskrivelse = obs.Beskrivelse,
                    Lokasjon = obs.Lokasjon
                }).ToListAsync();
                return alleObservasjoner;
            }
            catch { return null; }
        }

        public async Task<Observasjon> HentEn(int id)
        {
            try
            {
                Observasjoner enObservasjon = await _db.Observasjoner.FindAsync(id);
                var hentetObservasjon = new Observasjon()
                {
                    Id = enObservasjon.Id,
                    Navn = enObservasjon.Navn,
                    Dato = enObservasjon.Dato,
                    Tid = enObservasjon.Tid,
                    Lokasjon = enObservasjon.Lokasjon,
                    Beskrivelse = enObservasjon.Beskrivelse,
                };
                return hentetObservasjon;
            }
            catch { return null; }
        }

        public async Task<bool> Endre(Observasjon endreObservasjon)
        {
            var enObservasjon = await _db.Observasjoner.FindAsync(endreObservasjon.Id);
            try
            {
                enObservasjon.Navn = endreObservasjon.Navn;
                enObservasjon.Tid = endreObservasjon.Tid;
                enObservasjon.Dato = endreObservasjon.Dato;
                enObservasjon.Beskrivelse = endreObservasjon.Beskrivelse;
                enObservasjon.Lokasjon = endreObservasjon.Lokasjon;

                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Observasjoner enObservasjon = await _db.Observasjoner.FindAsync(id);
                _db.Observasjoner.Remove(enObservasjon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
    }
}
