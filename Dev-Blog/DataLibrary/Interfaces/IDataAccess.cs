using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataLibrary.Interfaces
{
    public interface IDataAccess
    {
        Task<List<T>> LoadData<T, U>(string table, U paramters, string connectionStr);

        Task SaveData<T>(string sql, T paramters, string connectionStr);

        Task<T> GetLatest<T>(string table, string connectionStr);

        Task<string> AddImgToDropBox<T>(T model, IFormFile file, string fileName);
    }
}