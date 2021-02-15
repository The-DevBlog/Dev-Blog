﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [Required]
        public string UpdateNum { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string ImgURL { get; set; }

        public List<UpVoteModel> UpVotes { get; set; }
        public List<DownVoteModel> DownVotes { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}