using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }

        public Post Post { get; set; }
    }
}