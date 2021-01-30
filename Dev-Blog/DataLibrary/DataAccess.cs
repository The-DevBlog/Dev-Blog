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
        public async Task<List<T>> LoadData<T, U>(string sql, U paramters, string connectionStr)
        {
            using (IDbConnection connection = new MySqlConnection(connectionStr))
            {
                var rows = await connection.QueryAsync<T>(sql, paramters);
                return rows.ToList();
            }
        }

        public Task SaveData<T>(string sql, T paramters, string connectionStr)
        {
            using (IDbConnection connection = new MySqlConnection(connectionStr))
            {
                return connection.ExecuteAsync(sql, paramters);
            }
        }

        //public Task UpdateDB<T>(string sql, T paramters)
        //{
        //}
    }
}