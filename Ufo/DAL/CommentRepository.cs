using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public class CommentRepository : InterfaceCommentRepository
    {
        private readonly ObservationContext _db;

        public CommentRepository(ObservationContext db)
        {
            _db = db;
        }

        public async Task<List<Comment>> FetchAllComments(int observationId)
        {
            try
            {
                // henter ut kommentarene som hører til observasjonen
                List<Comment> allComments = await _db.Comments
                    .Where(c => c.Observations.Id == observationId)
                    .Select(c => new Comment
                    {
                        Id = c.Id,
                        Text = c.Text,
                        ObservationId = c.Observations.Id,
                        UpVote = c.UpVote,
                        DownVote = c.Downvote,
                    }).ToListAsync();

                return allComments;
            }
            catch { return null; }
        }

        public async Task<bool> AddComment(Comment inComment)
        {
            try
            {
                var newCommentRow = new Comments();
                newCommentRow.Text = inComment.Text;

                // setter brukeren
                //var user = _db.Users.Where(u => inComment.Username == u.Username);
                //newCommentRow.Users = (Users)user;

                // henter observasjonen som kommentaren skal knyttes til
                var enObservasjon = await _db.Observations.FindAsync(inComment.ObservationId);

                // knytter kommentaren og observasjonen sammen
                newCommentRow.Observations = enObservasjon;

                _db.Comments.Add(newCommentRow);
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> DeleteComment(int id)
        {
            try
            {
                Comments comment = await _db.Comments.FindAsync(id);
                _db.Comments.Remove(comment);

                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpVote(int id)
        {
            try
            {
                Comments comment = await _db.Comments.FindAsync(id);
                comment.UpVote += 1;

                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> DownVote(int id)
        {
            try
            {
                Comments comment = await _db.Comments.FindAsync(id);
                comment.Downvote += 1;

                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
    }
}