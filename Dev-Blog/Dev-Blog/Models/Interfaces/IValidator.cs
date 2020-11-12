using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev_Blog.Models.Interfaces
{
    public interface IValidator
    {
        // TODO: summary comments
        bool UserNameExists(string userName);

        bool EmailExists(string email);

        string ValidateComment(string comment);
    }
}