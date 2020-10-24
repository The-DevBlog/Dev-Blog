using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IVote
    {
        public Task CreateUpVote(UpVote vote);

        public Task CreateDownVote(DownVote vote);

        public Task DeleteDownVote(DownVote vote);

        public Task DeleteUpVote(UpVote vote);

        public Task<bool> HasUpVoted(UpVote vote);

        public Task<bool> HasDownVoted(DownVote vote);
    }
}