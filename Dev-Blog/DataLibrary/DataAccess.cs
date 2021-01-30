using Dapper;
using DataLibrary.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class DataAccess : IDataAccess
    {
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
            string sql = $"SELECT * FROM {table} LIMIT 1;";

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

        //public Task UpdateDB<T>(string sql, T paramters)
        //{
        //}
    }
}