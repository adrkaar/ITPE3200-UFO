using Microsoft.AspNetCore.Mvc;
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

        public CommentController(InterfaceCommentRepository db)
        {
            _db = db;
        }

        [HttpGet("fetchAllComments/{id}")]
        public async Task<ActionResult> FetchAllComments(int id)
        {
            List<Comment> allComments = await _db.FetchAllComments(id);
            return Ok(allComments);
        }

        [HttpPost("addComment")]
        public async Task<ActionResult> AddComment(Comment inComment)
        {
            return Ok(await _db.AddComment(inComment));
        }

        [HttpDelete("deleteComment/{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            return Ok(await _db.DeleteComment(id));
        }

        [HttpGet("upVote/{id}")]
        public async Task<ActionResult> UpVote(int id)
        {
            return Ok(await _db.UpVote(id));
        }

        [HttpGet("downVote/{id}")]
        public async Task<ActionResult> DownVote(int id)
        {
            return Ok(await _db.DownVote(id));
        }
    }
}
