using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ufo.DAL;
using Ufo.Models;

namespace Ufo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly InterfaceCommentRepository _db;
        private ILogger<CommentController> _log;

        public CommentController(InterfaceCommentRepository db, ILogger<CommentController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet("fetchAllComments/{id}")]
        public async Task<ActionResult> FetchAllComments(int id)
        {
            List<Comment> allComments = await _db.FetchAllComments(id);
            if (allComments == null)
            {
                _log.LogInformation("Table in database is empty");
                return BadRequest("Table in database is empty");
            }
            return Ok(allComments);
        }

        [HttpPost("addComment")]
        public async Task<ActionResult> AddComment(Comment inComment)
        {
            if (ModelState.IsValid)
            {
                bool returnOk = await _db.AddComment(inComment);
                if (!returnOk)
                {
                    _log.LogInformation("Could not save comment");
                    return BadRequest("Could not save comment");
                }
                return Ok(returnOk);
            }
            else
            {
                return BadRequest("Error in input validation");
            }
        }

        [HttpDelete("deleteComment/{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            bool returnOk = await _db.DeleteComment(id);
            if (!returnOk)
            {
                _log.LogInformation("Could not delete comment");
                return BadRequest("Could not delete comment");
            }
            return Ok(returnOk);
        }

        [HttpGet("upVote/{id}")]
        public async Task<ActionResult> UpVote(int id)
        {
            bool returnOk = await _db.UpVote(id);
            if (!returnOk)
            {
                _log.LogInformation("Could not up vote");
                return BadRequest("Could not up vote");
            }
            return Ok(returnOk);
        }

        [HttpGet("downVote/{id}")]
        public async Task<ActionResult> DownVote(int id)
        {
            bool returnOk = await _db.DownVote(id);
            if (!returnOk)
            {
                _log.LogInformation("Could not down vote");
                return BadRequest("Could not down vote");
            }
            return Ok(returnOk);
        }
    }
}
