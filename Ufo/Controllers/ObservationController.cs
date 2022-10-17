using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Observation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObservationController: ControllerBase
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
                var nyObservasjonRad = new Observasjon();                                 // Aa lagre ny observasjon raad
                nyObservasjonRad.Dato = innObservasjon.Dato;
                nyObservasjonRad.Navn = innObservasjon.Navn;
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
            } catch
     
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
                    Tid =obs.Tid,
                    Beskrivelse = obs.Beskrivelse,
                    Lokasjon = obs.Lokasjon

                    //  IdUfo = obs.IdUfo,                         // Data of 1 UFO are inserted in Observasjon-table
                    //  NavnUfo = obs.NavnUfo                     //Tror Oleksandra prøvde å koble til models/ufo
                }).ToListAsync();
                return alleObservasjoner;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("fetchOneObservation")]
        public async Task <Observasjon> HentEn (int id)                                  // Hente en Observasjon paa ID
        {
            try
            {
                Observasjon enObservasjon = await _db.Observasjoner.FindAsync(id);
                var hentetObservasjon = new Observasjon()
                {
                    Id = enObservasjon.Id,
                    Dato = enObservasjon.Dato,
                    Navn=enObservasjon.Navn,
                    Tid = enObservasjon.Tid,
                    Beskrivelse = enObservasjon.Beskrivelse,

                   // IdUfo = enObservasjon.UFO.IdUfo,                            // Data of 1 UFO are inserted in Observasjon-table
                   // NavnUfo = enObservasjon.UFO.NavnUfo
                };                
                return hentetObservasjon;
            }
            catch
            {
                return null;
            }
        }

        [HttpPost("editObservation")]
        public async Task<bool> Endre (Observasjon endreObservasjon)                // Gi mulighet til aa endre alle linjer i en Observasjon
        {
            try
            {
                Observasjon enObservasjon = await _db.Observasjoner.FindAsync(endreObservasjon.Id);

                // Aa finne ut om navnUfo er endret: a person made a mistake, when he/she wrote data in the table (gave wrong identification of UFO)
                // if (enObservasjon.UFO.NavnUfo != endreObservasjon.NavnUfo || enObservasjon.UFO.IdUfo != endreObservasjon.IdUfo);
                {
                    /* 
                    var sjekkUfoEn = await _db.Ufoer.FindAsync(endreObservasjon.IdUfo);
                    var sjekkUfoTo = await _db.Ufoer.FindAsync(endreObservasjon.NavnUfo);                 //Het sjeffUfoEn tidligere

                    if (sjekkUfoEn == null)                                                               // UFO ikke finnes
                    {
                        var nyUfoRad = new Ufoer();
                        // nyUfoRad.IdUfo = endreObservasjon.IdUfo;                                      // To add IdUfo, NavnUfo to our table Observasjon. But typeUfo and beskrivelseUfo will be seen only in Ufo-table. 
                        // nyUfoRad.NavnUfo = endreObservasjon.NavnUfo;

                        /* If we want to see in the database Type and Beskrivelse too ---> to delete comments for the rows below (So, we'll add them to our table Observasjon):
                        nyUfoRad.TypeUfo = innObservasjon.TypeUfo;
                        nyUfoRad.BeskrivelseUfo = innObservasjon.BeskrivelseUfo;
                        */
                    /*
                        enObservasjon.UFO = nyUfoRad;                               // Aa lagre UFO i Observasjon
                    }
                    else
                    {
                        enObservasjon.UFO = sjekkUfoEn;
                    }
                    */

                }

                enObservasjon.Dato = endreObservasjon.Dato;
                enObservasjon.Navn = endreObservasjon.Navn;
                enObservasjon.Tid = endreObservasjon.Tid;
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

        [HttpDelete("deleteObservation")]
        public async Task <bool> Slett(int id)                                       // Slett en Observasjon paa ID
        {
            try
            {
                // Observasjoner enObservasjon = await _db.Observasjoner.FindAsync(id);
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
