using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    public interface IEmailRepository
    {
        Task Welcome(string email);

        Task NewPost(string img = null);
    }
}