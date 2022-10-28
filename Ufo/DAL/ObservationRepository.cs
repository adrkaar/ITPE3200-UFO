using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public class ObservationRepository : InterfaceObservationRepository
    {
        private readonly ObservationContext _db;

        public ObservationRepository(ObservationContext db)
        {
            _db = db;
        }

        public async Task<bool> SaveObservation(Observation inObservation)
        {
            try
            {
                var newObservationRow = new Observations();

                newObservationRow.Date = inObservation.Date;
                newObservationRow.Time = inObservation.Time;
                newObservationRow.Description = inObservation.Description;
                newObservationRow.Latitude = inObservation.Latitude;
                newObservationRow.Longitude = inObservation.Longitude;

                // henter ufo objekt fra ufo tabell 
                var ufo = _db.UfoTypes.Where(u => inObservation.UfoType.Contains(u.Type)).FirstOrDefault();

                // hvis ufo ikke finnes i databasen blir den lagt til
                if (ufo == null)
                {
                    string ufoType = inObservation.UfoType;
                    ufo = await AddUfoType(ufoType);
                }

                // setter den nye raden sin ufo til hentet ufo
                newObservationRow.UfoTypes = ufo;

                _db.Observations.Add(newObservationRow);
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<List<Observation>> FetchAllObservations()
        {
            try
            {
                List<Observation> allObservations = await _db.Observations.Select(obs => new Observation
                {
                    Id = obs.Id,
                    Date = obs.Date,
                    Time = obs.Time,
                    Description = obs.Description,
                    Latitude = obs.Latitude,
                    Longitude = obs.Longitude,
                    UfoType = obs.UfoTypes.Type
                }).ToListAsync();
                return allObservations;
            }
            catch { return null; }
        }

        public async Task<Observation> GetOneObservation(int id)
        {
            try
            {
                Observations oneObservation = await _db.Observations.FindAsync(id);
                var fetchedObservation = new Observation()
                {
                    Id = oneObservation.Id,
                    UfoType = oneObservation.UfoTypes.Type,
                    Date = oneObservation.Date,
                    Time = oneObservation.Time,
                    Latitude = oneObservation.Latitude,
                    Longitude = oneObservation.Longitude,
                    Description = oneObservation.Description,
                };
                return fetchedObservation;
            }
            catch { return null; }
        }

        public async Task<bool> ChangeObservation(Observation changeObservation)
        {
            var oneObservation = await _db.Observations.FindAsync(changeObservation.Id);
            try
            {
                oneObservation.Time = changeObservation.Time;
                oneObservation.Date = changeObservation.Date;
                oneObservation.Description = changeObservation.Description;
                oneObservation.Latitude = changeObservation.Latitude;
                oneObservation.Longitude = changeObservation.Longitude;

                // henter ufo objekt fra ufo tabell 
                var ufo = _db.UfoTypes.Where(u => changeObservation.UfoType.Contains(u.Type)).FirstOrDefault();

                // hvis ufo ikke finnes i tabellen blir den lagt til
                if (ufo == null)
                {
                    string ufoType = changeObservation.UfoType;
                    ufo = await AddUfoType(ufoType);
                }

                // setter den nye raden sin ufo til hentet ufo
                oneObservation.UfoTypes = ufo;

                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> DeleteObservation(int id)
        {
            try
            {
                Observations oneObservation = await _db.Observations.FindAsync(id);

                CommentRepository cRepo = new CommentRepository(_db);

                // henter kommentarene til observasjonen, slik at de kan bli slettet først
                var comments = cRepo.FetchAllComments(id);

                // går igjennom og sletter kommentarene
                foreach (Comment comment in comments.Result.ToList())
                {
                    await cRepo.DeleteComment(comment.Id);
                }

                _db.Observations.Remove(oneObservation);
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<List<UfoType>> FetchUfoTypes()
        {
            try
            {
                List<UfoType> ufoTypes = await _db.UfoTypes.Select(type => new UfoType
                {
                    Type = type.Type
                }).ToListAsync();
                return ufoTypes;
            }
            catch { return null; }
        }

        public async Task<UfoTypes> AddUfoType(string newUfoType)
        {
            try
            {
                var ufoType = new UfoTypes();
                ufoType.Type = newUfoType;

                _db.UfoTypes.Add(ufoType);
                await _db.SaveChangesAsync();
                return ufoType;
            }
            catch { return null; }
        }
    }
}
