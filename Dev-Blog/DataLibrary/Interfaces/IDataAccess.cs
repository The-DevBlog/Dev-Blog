using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Interfaces
{
    public interface IDataAccess
    {
        Task<List<T>> LoadData<T, U>(string table, U paramters, string connectionStr);

        Task SaveData<T>(string sql, T paramters, string connectionStr);

        Task<T> GetLatest<T>(string table, string connectionStr);
    }
}