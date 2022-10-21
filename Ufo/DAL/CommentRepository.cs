﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ufo.Models;

namespace Ufo.DAL
{
    public class CommentRepository : InterfaceCommentRepository
    {
        private readonly ObservasjonContext _db;

        public CommentRepository(ObservasjonContext db)
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
                        ObservationId = c.Observations.Id
                    }).ToListAsync();

                return allComments;
            }
            catch { return null; }
        }

        public async Task<bool> AddComment(Comment inComment)
        {
            try
            {
                // laget objektet
                var newCommentRow = new Comments();
                // setter teksten
                newCommentRow.Text = inComment.Text;

                var enObservasjon = await _db.Observasjoner.FindAsync(inComment.ObservationId);
                Console.WriteLine(enObservasjon);

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

                //if (employeeToDelete == null)
                //{
                //    return NotFound($"Employee with Id = {id} not found");
                //}

                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
    }
}
