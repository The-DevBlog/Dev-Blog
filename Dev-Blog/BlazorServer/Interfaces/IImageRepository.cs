using System.IO;
using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    public interface IImageRepository
    {
        Task<string> AddImgToDropBox(Stream fs, string name);
    }
}