using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Models;
using Dev_Blog.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Blog.Controllers
{
    public class VMcontroller : Controller
    {
        private readonly IViewModel _viewModel;

        public VMcontroller(IViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public async Task<Post> GetLatestPost()
        {
            return await _viewModel.GetLatestPost();
        }
    }
}