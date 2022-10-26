using System.Collections.Generic;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public interface InterfaceObservationRepository
    {
        Task<List<Observation>> FetchAllObservations();
        Task<Observation> GetOneObservation(int id);
        Task<bool> SaveObservation(Observation inObservation);
        Task<bool> DeleteObservation(int id);
        Task<bool> ChangeObservation(Observation changeObservation);
        Task<List<UfoType>> FetchUfoTypes();
    }
}
