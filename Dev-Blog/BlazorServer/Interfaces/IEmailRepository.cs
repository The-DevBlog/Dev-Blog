using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    public interface IEmailRepository
    {
        Task Welcome(string email);

        Task NewPost(string img = null);

        Task<bool> CheckUsername(string username);

        Task<bool> CheckEmail(string email);
    }
}