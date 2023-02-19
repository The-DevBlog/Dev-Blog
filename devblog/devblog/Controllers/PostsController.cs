using Dev_Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace devblog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private static readonly IEnumerable<PostModel> Posts = new[]
        {
            new PostModel{
                Id= 1,
                UpdateNum= "Update 1",
                Description= "This is the first post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            },
            new PostModel{
                Id= 2,
                UpdateNum= "Update 2",
                Description= "This is the second post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            },
            new PostModel{
                Id= 3,
                UpdateNum= "Update 3",
                Description= "This is the third post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            },
            new PostModel{
                Id= 4,
                UpdateNum= "Update 4",
                Description= "This is the fourth post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            },
            new PostModel{
                Id= 5,
                UpdateNum= "Update 5",
                Description= "This is the fifth post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            },
            new PostModel{
                Id= 6,
                UpdateNum= "Update 6",
                Description= "This is the sixth post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            },
            new PostModel{
                Id= 7,
                UpdateNum= "Update 7",
                Description= "This is the seventh post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            },
            new PostModel{
                Id= 8,
                UpdateNum= "Update 8",
                Description= "This is the eighth post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            },
            new PostModel{
                Id= 9,
                UpdateNum= "Update 9",
                Description= "This is the ninth post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            },
            new PostModel{
                Id= 10,
                UpdateNum= "Update 10",
                Description= "This is the tenth post.",
                Date= new DateTime(2023, 02, 01),
                ImgURL= "http://place-hold.it/900x600"
            }
        };

        [HttpGet("get")]
        public IEnumerable<PostModel> Get()
        {
            return Posts;
        }
    }
}

