using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public interface InterfaceUserRepository
    {
        Task<bool> LogIn(User user);
    }
}