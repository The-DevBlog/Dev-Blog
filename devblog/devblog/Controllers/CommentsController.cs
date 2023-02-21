using devblog.Interfaces;
using devblog.Models;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _comments;

        public CommentsController(ICommentService comments)
        {
            _comments = comments;
        }

        /// <summary>
        /// Adds a comment
        /// </summary>
        /// <param name="comment">The comment to add</param>
        /// <returns>Comment</returns>
        [HttpPost]
        public async Task<Comment> Create(Comment comment)
        {
            var newComment = await _comments.Create(comment.Content, comment.UserName, comment.PostId);
            return newComment;
        }
    }
}

