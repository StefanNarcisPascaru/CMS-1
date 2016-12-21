using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Domain.Models;

namespace CMS.BussinesInterfaces.ModelLogic
{
    public interface IUserLogic
    {
        User GetUser(Guid id);
        bool IsValidUser(string user, string password);
    }
}
