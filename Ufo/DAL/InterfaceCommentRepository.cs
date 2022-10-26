using System.Collections.Generic;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public interface InterfaceCommentRepository
    {
        Task<List<Comment>> FetchAllComments(int observationId);
        Task<bool> AddComment(Comment inComment);
        Task<bool> DeleteComment(int id);
        Task<bool> UpVote(int id);
        Task<bool> DownVote(int id);
    }
}