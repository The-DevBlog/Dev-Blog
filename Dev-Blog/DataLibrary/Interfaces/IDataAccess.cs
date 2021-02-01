using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DataLibrary.Interfaces
{
    public interface IDataAccess
    {
        Task<List<P>> LoadData<P, C, U>(U paramters, string connectionStr);

        Task SaveData<T>(string sql, T paramters, string connectionStr);

        Task<T> GetLatest<T>(string table, string connectionStr);

        Task<string> AddImgToDropBox(Stream fs, string fileName);
    }
}