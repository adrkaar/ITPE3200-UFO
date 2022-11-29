using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public class ObservationRepository : InterfaceObservationRepository
    {
        private readonly ObservationContext _db;
        private ILogger<ObservationRepository> _log;

        public ObservationRepository(ObservationContext db, ILogger<ObservationRepository> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<bool> SaveObservation(Observation inObservation)
        {
            try
            {
                var newObservationRow = new Observations();
                newObservationRow.Headline = inObservation.Headline;
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
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<List<Observation>> FetchAllObservations()
        {
            try
            {
                List<Observation> allObservations = await _db.Observations.Select(obs => new Observation
                {
                    Id = obs.Id,
                    Headline = obs.Headline,
                    Date = obs.Date,
                    Time = obs.Time,
                    Description = obs.Description,
                    Latitude = obs.Latitude,
                    Longitude = obs.Longitude,
                    UfoType = obs.UfoTypes.Type,
                }).OrderByDescending(Date => Date.Date).ToListAsync();
                return allObservations;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<Observation> GetOneObservation(int id)
        {
            try
            {
                Observations oneObservation = await _db.Observations.FindAsync(id);
                var fetchedObservation = new Observation()
                {
                    Id = oneObservation.Id,
                    Headline = oneObservation.Headline,
                    UfoType = oneObservation.UfoTypes.Type,
                    Date = oneObservation.Date,
                    Time = oneObservation.Time,
                    Latitude = oneObservation.Latitude,
                    Longitude = oneObservation.Longitude,
                    Description = oneObservation.Description,
                };
                return fetchedObservation;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<bool> ChangeObservation(Observation changeObservation)
        {
            var oneObservation = await _db.Observations.FindAsync(changeObservation.Id);
            try
            {
                oneObservation.Headline = changeObservation.Headline;
                oneObservation.Date = changeObservation.Date;
                oneObservation.Time = changeObservation.Time;
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
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
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
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
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
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
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
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }
    }
}