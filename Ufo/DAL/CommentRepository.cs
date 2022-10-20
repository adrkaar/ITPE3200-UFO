using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public class CommentRepository: InterfaceCommentRepository
    {
        private readonly ObservasjonContext _db;

        public CommentRepository(ObservasjonContext db)
        {
            _db = db;
        }

        public async Task<List<Comment>> FetchAllComments()
        {
            try
            {
                List<Comment> allComments = await _db.Comments.Select(c => new Comment
                {
                    Id = c.Id,
                    Text = c.Text
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
                // var objekt = _db.Comments.Join(_db.Observasjoner, ObservationId => Id); 
                // var objekt = _db.Observasjoner.Join(_db.Comments, x => x.Id, y => y.Id, (x, y) => new { () }.ToList();

                newCommentRow.Id = inComment.Id;
                newCommentRow.Text = inComment.Text;
                //newCommentRow.Observation.Id = inComment.ObservationId;

                var enObservasjon = await _db.Observasjoner.FindAsync(inComment);
                newCommentRow.Observation = enObservasjon;

                // med denne ^ så må frontend observation model inneholde obsevasjonsid

                // må koble kommentar til objekt?
                // den adder kunn id og tekst, må på en eller annen måte koble til observasjon
                // skal koblingen skje i frontend?? må skje i frontend, eneste som kan vite hvilken observasjon kommentaren hører til?

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
    }
}
