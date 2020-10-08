﻿using Dev_Blog.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Post> Posts { get; set; }
        public Post LatestPost { get; set; }
    }
}