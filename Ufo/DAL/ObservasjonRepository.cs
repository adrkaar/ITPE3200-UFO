using Ufo.Models;

namespace Ufo.DAL
{

    public class ObservasjonRepository
    {
        private readonly ObservasjonContext _db;
        public ObservationController(ObservasjonContext db)
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
        public async Task<bool> Lagre(Observasjon innObservasjon)                         // To insert data from a small Observasjon-table into our huge Observasjoner-table      
        {
            try
            {
                var nyObservasjonRad = new Observasjoner();                                 // Aa lagre ny observasjon raad
                nyObservasjonRad.Navn = innObservasjon.Navn;
                nyObservasjonRad.Dato = innObservasjon.Dato;
                nyObservasjonRad.Tid = innObservasjon.Tid;
                nyObservasjonRad.Beskrivelse = innObservasjon.Beskrivelse;
                nyObservasjonRad.Lokasjon = innObservasjon.Lokasjon;

                // Aa sjekke om UFO eksistere i databasen:
                /*
                var sjekkUfoEn = await _db.Ufoer.FindAsync(innObservasjon.IdUfo);                                                 //    ^^ Search only by IdUfo? ^^ 
                if (sjekkUfoEn == null)                                                               // UFO ikke finnes
                {
                    var nyUfoRad = new Ufoer();
                    nyUfoRad.IdUfo = innObservasjon.IdUfo;                                      // To add IdUfo, NavnUfo to our table Observasjon. But typeUfo and beskrivelseUfo will be seen only in Ufo-table. 
                    nyUfoRad.NavnUfo = innObservasjon.NavnUfo;

                    /* If we want to see in the database Type and Beskrivelse too ---> to delete comments for the rows below (So, we'll add them to our table Observasjon):
                    nyUfoRad.TypeUfo = innObservasjon.TypeUfo;
                    nyUfoRad.BeskrivelseUfo = innObservasjon.BeskrivelseUfo;
                    */
                /*
                nyObservasjonRad.UFO = nyUfoRad;                               // Aa lagre UFO i Observasjon
            }
            else
            {
                nyObservasjonRad.UFO = sjekkUfoEn;
            }
            */
                _db.Observasjoner.Add(nyObservasjonRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch

            {
                return false;
            }
        }

        [HttpGet("fetchAllObservations")]
        public async Task<List<Observasjon>> HentAlle()
        {
            try
            {
                List<Observasjon> alleObservasjoner = await _db.Observasjoner.Select(obs => new Observasjon
                {
                    Id = obs.Id,
                    Dato = obs.Dato,
                    Navn = obs.Navn,
                    Tid = obs.Tid,
                    Beskrivelse = obs.Beskrivelse,
                    Lokasjon = obs.Lokasjon

                }).ToListAsync();
                return alleObservasjoner;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("fetchOneObservation{id}")]
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
            catch
            {
                return null;
            }
        }

        [HttpPost("editObservation")]
        public async Task<bool> Endre(Observasjon endreObservasjon)
        {
            try
            {
                Observasjoner enObservasjon = await _db.Observasjoner.FindAsync(endreObservasjon.Id);

                enObservasjon.Navn = endreObservasjon.Navn;
                enObservasjon.Tid = endreObservasjon.Tid;
                enObservasjon.Dato = endreObservasjon.Dato;
                enObservasjon.Beskrivelse = endreObservasjon.Beskrivelse;
                enObservasjon.Lokasjon = endreObservasjon.Lokasjon;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete("deleteObservation{id}")]
        public async Task<bool> Slett(int id)
        {
            try
            {
                Observasjoner enObservasjon = await _db.Observasjoner.FindAsync(id);
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
