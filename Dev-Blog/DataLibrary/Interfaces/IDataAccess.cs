using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Interfaces
{
    public interface IDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U paramters, string connectionStr);

        Task SaveData<T>(string sql, T paramters, string connectionStr);
    }
}