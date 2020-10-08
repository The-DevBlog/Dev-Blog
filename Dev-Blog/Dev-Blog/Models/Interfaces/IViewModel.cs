using Dev_Blog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IViewModel
    {
        Task<Post> GetLatestPost();
    }
}