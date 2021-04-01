using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Interfaces
{
    public interface IAppState
    {
        bool CheckUsername(string username);

        string[] GetUsers();
    }
}