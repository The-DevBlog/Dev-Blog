using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    public interface IEmails
    {
        Task Welcome(string email);
    }
}