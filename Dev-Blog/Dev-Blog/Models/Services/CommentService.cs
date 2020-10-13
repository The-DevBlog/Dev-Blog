using Dev_Blog.Data;
using Dev_Blog.Models.Interfaces;
using Dropbox.Api.CloudDocs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Services
{
    public class CommentService : IComment
    {
        private readonly AppDbContext _context;
        private readonly IPost _post;

        public CommentService(IPost post, AppDbContext context)
        {
            _post = post;
            _context = context;
        }

        /// <summary>
        /// Adds a comment to the database
        /// </summary>
        /// <param name="userId">Id of the user associated with the comment</param>
        /// <param name="post">Post that is being commented on</param>
        /// <param name="content">The content of the comment</param>
        /// <returns>Successful completion of task</returns>
        public async Task<Comment> Create(string userId, Post post, string content)
        {
            Comment comment = new Comment()
            {
                UserId = userId,
                PostId = post.Id,
                Content = content,
                Date = DateTime.Now,
                Post = post
            };
            _context.Entry(comment).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return comment;
        }

        // TODO: sumamry comment
        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comment.OrderByDescending(x => x.Date)
                                            .ToListAsync();
        }

        /// <summary>
        /// Retrieves the comments for a specified post
        /// </summary>
        /// <param name="postId">The id of the post</param>
        /// <returns>All comments for post</returns>
        public async Task<List<Comment>> CommentsForPost(int postId)
        {
            List<Comment> comments = await _context.Comment.
                Where(x => x.Post.Id == postId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return comments;
        }
    }
}