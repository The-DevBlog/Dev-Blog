using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models
{
    public class DownVote
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public Post Post { get; set; }
    }
}