using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public class ObservationRepository : InterfaceObservationRepository
    {
        private readonly ObservasjonContext _db;

        public ObservationRepository(ObservasjonContext db)
        {
            _db = db;
        }

        public async Task<bool> SaveObservation(Observation inObservation)
        {
            try
            {
                var newObservationRow = new Observations();

                newObservationRow.Name = inObservation.Name;
                newObservationRow.Date = inObservation.Date;
                newObservationRow.Time = inObservation.Time;
                newObservationRow.Description = inObservation.Description;
                newObservationRow.Location = inObservation.Location;

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
                    Name = obs.Name,
                    Date = obs.Date,
                    Time = obs.Time,
                    Description = obs.Description,
                    Location = obs.Location
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
                    Name = oneObservation.Name,
                    Date = oneObservation.Date,
                    Time = oneObservation.Time,
                    Location = oneObservation.Location,
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
                oneObservation.Name = changeObservation.Name;
                oneObservation.Time = changeObservation.Time;
                oneObservation.Date = changeObservation.Date;
                oneObservation.Description = changeObservation.Description;
                oneObservation.Location = changeObservation.Location;

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
                var comments = cRepo.FetchAllComments(id);

                // går igjennom comments og sletter hver kommentar som hører til observasjonen
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
    }
}
