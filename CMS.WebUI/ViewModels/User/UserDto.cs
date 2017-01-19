using System.Collections.Generic;
using CMS.Domain.Models;

namespace CMS.WebUI.ViewModels.User
{
    public class UserDto
    {
        public Domain.Models.User User { get; set; }
        public IList<Rank> Ranks { get; set; }
     }
}
