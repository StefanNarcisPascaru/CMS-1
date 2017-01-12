using System;
using System.Collections.Generic;
using CMS.Domain.Models;

namespace CMS.BussinesInterfaces.ModelLogic
{
    public interface IUserLogic
    {
        User GetUser(Guid id);
        bool IsValidUser(string user, string password);

        ICollection<string> GetRanks(string user);

        ICollection<User> GetUsers();

        void Update(User user, IList<Rank> ranks);
    }
}
