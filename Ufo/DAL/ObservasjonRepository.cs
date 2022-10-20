namespace Ufo.DAL
{

    public class ObservasjonRepository : InterfaceObservasjonRepository
    {
        private readonly ObservasjonContext _db;

        public ObservasjonRepository(ObservasjonContext db)
        {
            _db = db;
        }

        public async Task<bool> Lagre(Observasjon innObservasjon)
        {
            try
            {
                var nyObservasjonRad = new Observasjoner();

                nyObservasjonRad.Navn = innObservasjon.Navn;
                nyObservasjonRad.Dato = innObservasjon.Dato;
                nyObservasjonRad.Tid = innObservasjon.Tid;
                nyObservasjonRad.Beskrivelse = innObservasjon.Beskrivelse;
                nyObservasjonRad.Lokasjon = innObservasjon.Lokasjon;

                _db.Observasjoner.Add(nyObservasjonRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<List<Observasjon>> HentAlle()
        {
            try
            {
                List<Observasjon> alleObservasjoner = await _db.Observasjoner.Select(obs => new Observasjon
                {
                    Id = obs.Id,
                    Navn = obs.Navn,
                    Dato = obs.Dato,
                    Tid = obs.Tid,
                    Beskrivelse = obs.Beskrivelse,
                    Lokasjon = obs.Lokasjon
                }).ToListAsync();
                return alleObservasjoner;
            }
            catch { return null; }
        }

        public async Task<Observasjon> HentEn(int id)
        {
            try
            {
                Observasjoner enObservasjon = await _db.Observasjoner.FindAsync(id);
                var hentetObservasjon = new Observasjon()
                {
                    Id = enObservasjon.Id,
                    Navn = enObservasjon.Navn,
                    Dato = enObservasjon.Dato,
                    Tid = enObservasjon.Tid,
                    Lokasjon = enObservasjon.Lokasjon,
                    Beskrivelse = enObservasjon.Beskrivelse,
                };
                return hentetObservasjon;
            }
            catch { return null; }
        }

        public async Task<bool> Endre(Observasjon endreObservasjon)
        {
            var enObservasjon = await _db.Observasjoner.FindAsync(endreObservasjon.Id);
            try
            {
                enObservasjon.Navn = endreObservasjon.Navn;
                enObservasjon.Tid = endreObservasjon.Tid;
                enObservasjon.Dato = endreObservasjon.Dato;
                enObservasjon.Beskrivelse = endreObservasjon.Beskrivelse;
                enObservasjon.Lokasjon = endreObservasjon.Lokasjon;

                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Observasjoner enObservasjon = await _db.Observasjoner.FindAsync(id);
                _db.Observasjoner.Remove(enObservasjon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        /* Comment ******************************************************************/
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

                newCommentRow.Id = inComment.Id;
                newCommentRow.Text = inComment.Text;
                newCommentRow.Observation.Id = inComment.ObservationId;
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
                return true;
            }
            catch { return false; }
        }
    }
}
