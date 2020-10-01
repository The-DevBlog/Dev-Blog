using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models
{
    public class NewPost
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
    }
}