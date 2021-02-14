using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    public interface IVotes
    {
        Task UpVote(int postId, string username);

        Task DownVote(int postId, string username);
    }
}