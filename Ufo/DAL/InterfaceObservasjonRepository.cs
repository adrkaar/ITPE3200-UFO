using System.Collections.Generic;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public interface InterfaceObservasjonRepository
    {
        /* Observation ******************************************************************/
        Task<bool> Lagre(Observasjon innObservasjon);
        Task<bool> Slett(int id);
        Task<Observasjon> HentEn(int id);
        Task<bool> Endre(Observasjon endreObservasjon);
        Task<List<Observasjon>> HentAlle();

        /* Comment ******************************************************************/
        Task<List<Comment>> FetchAllComments();
        Task<bool> AddComment(Comment inComment);
        Task<bool> DeleteComment(int id);
    }
}
