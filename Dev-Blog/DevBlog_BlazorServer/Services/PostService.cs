using Dapper;
using DevBlog_BlazorServer.Interfaces;
using DevBlog_BlazorServer.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
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

        public async Task<List<PostModel>> GetPosts()
        {
            string sql = $"SELECT * FROM post " +
                         $"LEFT JOIN comment " +
                         $"ON post.Id = comment.PostId;";

            var postsDict = new Dictionary<int, PostModel>();
            var posts = new List<PostModel>();
            using (IDbConnection cnn = new MySqlConnection(_config.GetConnectionString("DevBlogDB")))
            {
                // TODO: I dont fully understand this
                cnn.Query<PostModel, CommentModel, PostModel>(sql,
                    (post, comment) =>
                    {
                        if (!postsDict.ContainsKey(post.Id))
                            postsDict.Add(post.Id, post);

                        var cach = postsDict[post.Id];

                        if (cach.Comments == null)
                            cach.Comments = new List<CommentModel>();

                        cach.Comments.Add(comment);
                        posts.Add(cach);

                        return cach;
                    }, splitOn: "PostId");
            }

            posts.Reverse();
            return posts;
        }
    }
}