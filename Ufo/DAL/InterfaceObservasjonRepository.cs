using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public interface InterfaceObservasjonRepository
    {
        Task<bool> Lagre(Observasjoner observasjoner);
        Task<bool> Slett(int id);
        Task<Observasjoner> HentEn(int id);
        Task<bool> Endre(Observasjoner observasjoner);
        Task<List<Observasjon>> HentAlle();
    }
}
