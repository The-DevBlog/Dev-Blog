using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IVote
    {
        public Task Create(Vote vote);

        public Task Delete(Vote vote);
    }
}