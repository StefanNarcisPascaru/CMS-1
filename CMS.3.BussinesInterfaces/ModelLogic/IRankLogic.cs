using System;
using System.Collections.Generic;
using CMS.Domain.Models;

namespace CMS.BussinesInterfaces.ModelLogic
{
    public interface IRankLogic
    {
        IList<Rank> GetUserRanks(Guid id);
    }
}
