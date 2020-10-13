using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IComment
    {
        // TODO: summary comments
        public Task Create(string userId, int postId, string content);

        // TODO: summary comments
        public Task<List<Comment>> CommentsForPost(int postId);
    }
}