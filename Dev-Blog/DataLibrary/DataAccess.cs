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

namespace DataLibrary
{
    public class DataAccess : IDataAccess
    {
        private IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string table, U parameters, string connectionStr)
        {
            string sql = $"SELECT * FROM {table};";

            using (IDbConnection connection = new MySqlConnection(connectionStr))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters);
                return rows.ToList();
            }
        }

        public async Task<T> GetLatest<T>(string table, string connectionStr)
        {
            string sql = $"SELECT * FROM {table} " +
                         "ORDER BY Id " +
                         "DESC LIMIT 1";

            using (IDbConnection connection = new MySqlConnection(connectionStr))
            {
                var row = await connection.QueryAsync<T>(sql);
                return row.FirstOrDefault();
            }
        }

        public Task SaveData<T>(string sql, T parameters, string connectionStr)
        {
            using (IDbConnection connection = new MySqlConnection(connectionStr))
            {
                return connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<string> AddImgToDropBox<T>(T model, IFormFile stream, string fileName)
        {
            string url = "";
            string dest = _config["DestinationPath"] + fileName;

            using (var dbx = new DropboxClient(_config["DropboxToken"]))
            {
                using (var fs = stream.OpenReadStream())
                {
                    var updated = await dbx.Files.UploadAsync(
                        dest,
                        WriteMode.Overwrite.Instance,
                        body: fs
                    );
                };

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