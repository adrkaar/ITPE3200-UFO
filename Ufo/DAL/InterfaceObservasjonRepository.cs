using System.Threading.Tasks;
using Ufo.Models;
using System.Collections.Generic;
using System;

namespace Ufo.DAL
{
    public interface InterfaceObservasjonRepository
    {
        Task<bool> Lagre(Observasjon innObservasjon);
        Task<bool> Slett(int id);
        Task<Observasjon> HentEn(int id);
        Task<bool> Endre(Observasjon endreObservasjon);
        Task<List<Observasjon>> HentAlle();
    }
}
