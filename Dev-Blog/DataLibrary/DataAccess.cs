using Dapper;
using DataLibrary.Interfaces;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace DataLibrary
{
    public class DataAccess : IDataAccess
    {
        private IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<Post>> LoadData<Post, Comment, U>(U parameters, string cnnStr)
        {
            string sql = $"SELECT * FROM post " +
                         $"LEFT JOIN comment " +
                         $"ON post.Id = comment.PostId;";

            using (IDbConnection cnn = new MySqlConnection(cnnStr))
            {
                var rows = await cnn.QueryAsync<Post>(sql, parameters);
                return rows.ToList();
            }
        }

        public async Task<T> GetLatest<T>(string table, string cnnStr)
        {
            string sql = $"SELECT * FROM {table} " +
                         "ORDER BY Id " +
                         "DESC LIMIT 1";

            using (IDbConnection cnn = new MySqlConnection(cnnStr))
            {
                var row = await cnn.QueryAsync<T>(sql);
                return row.FirstOrDefault();
            }
        }

        public Task SaveData<T>(string sql, T parameters, string cnnStr)
        {
            using (IDbConnection cnn = new MySqlConnection(cnnStr))
            {
                return cnn.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<string> AddImgToDropBox(Stream fs, string name)
        {
            // create unique file name
            string ext = Path.GetExtension(name);
            string fileName = $"{DateTime.Now.Ticks}{name}{ext}";

            string url = "";
            string dest = _config["DestinationPath"] + fileName;

            using (var dbx = new DropboxClient(_config["DropboxToken"]))
            {
                // upload file to dbx
                var updated = await dbx.Files.UploadAsync(
                    dest,
                    WriteMode.Overwrite.Instance,
                    body: fs
                );
                fs.Close();

                // create shareable link
                var link = dbx.Sharing.CreateSharedLinkWithSettingsAsync(dest);
                link.Wait();

                url = link.Result.Url;

                // remove id and replace with raw=1
                url = url.Substring(0, url.Length - 4) + "raw=1";
            }
            return url;
        }
    }
}