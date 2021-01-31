using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevBlog_BlazorServer.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string UpdateNum { get; set; }
        public DateTime Date { get; set; }
        public string ImgURL { get; set; }
        public string Description { get; set; }
        public int UpVotes { get; set; } = 0;
        public int DownVotes { get; set; } = 0;
        public List<CommentModel> Comments { get; set; }
    }
}