using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string UpdateNum { get; set; }
        public DateTime Date { get; set; }
        public string ImgURL { get; set; }
        public string Description { get; set; }
    }
}