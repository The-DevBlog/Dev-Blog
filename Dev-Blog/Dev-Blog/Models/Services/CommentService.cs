using Dev_Blog.Models.Interfaces;
using Dropbox.Api.CloudDocs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Services
{
    public class CommentService : IComment
    {
        public async Task Create(string userId, int postId, string content)
        {
            Comment comment = new Comment()
            {
                UserId = userId,
                PostId = postId,
                Content = content
            };
        }
    }
}