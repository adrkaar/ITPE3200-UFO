using System.Collections.Generic;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public interface InterfaceCommentRepository
    {
        Task<List<Comment>> FetchAllComments();
        Task<bool> AddComment(Comment inComment);
        Task<bool> DeleteComment(int id);
    }
}
