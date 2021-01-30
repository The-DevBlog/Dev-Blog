using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBlog_BlazorServer.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string UpdateNum { get; set; }
        public DateTime Date { get; set; }
        public string ImgURL { get; set; }
        public string Description { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        //public List<Comment> Comments { get; set; }
    }
}