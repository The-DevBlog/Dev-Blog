using Dev_Blog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Services
{
    public class VMservice : IViewModel
    {
        private readonly IPost _post;

        public VMservice(IPost post)
        {
            _post = post;
        }

        public Task<Post> GetLatestPost()
        {
            return _post.GetLatestPost();
        }
    }
}