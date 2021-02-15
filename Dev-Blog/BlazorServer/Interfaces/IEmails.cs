using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    public interface IEmails
    {
        Task Welcome(string email);

        Task NewPost(string img = null);
    }
}