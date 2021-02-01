using Dapper;
using DevBlog_BlazorServer.Interfaces;
using DevBlog_BlazorServer.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DevBlog_BlazorServer.Services
{
    public class PostService : IPostService
    {
        private IConfiguration _config;

        public PostService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Dictionary<int, PostModel>> GetPosts()
        {
            string sql = "SELECT * FROM post " +
                         "LEFT JOIN comment " +
                         "ON post.Id = comment.PostId " +
                         "ORDER BY post.Date DESC;";

            var posts = new Dictionary<int, PostModel>();
            using (IDbConnection cnn = new MySqlConnection(_config.GetConnectionString("DevBlogDB")))
            {
                // TODO: I dont fully understand this
                await cnn.QueryAsync<PostModel, CommentModel, PostModel>(sql,
                    (post, comment) =>
                    {
                        if (!posts.ContainsKey(post.Id))
                            posts.Add(post.Id, post);

                        var cach = posts[post.Id];

                        if (cach.Comments == null)
                            cach.Comments = new List<CommentModel>();

                        cach.Comments.Add(comment);

                        return cach;
                    }, splitOn: "PostId");
            }

            return posts;
        }
    }
}