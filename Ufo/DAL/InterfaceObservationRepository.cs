using System.Collections.Generic;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public interface InterfaceObservationRepository
    {
        Task<bool> SaveObservation(Observation inObservation);
        Task<bool> DeleteObservation(int id);
        Task<Observation> GetOneObservation(int id);
        Task<bool> ChangeObservation(Observation changeObservation);
        Task<List<Observation>> FetchAllObservations();
        Task<List<UfoType>> FetchUfoTypes();
    }
}
