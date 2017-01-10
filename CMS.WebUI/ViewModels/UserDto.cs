using System.Collections.Generic;
using CMS.Domain.Models;

namespace CMS.WebUI.ViewModels
{
    public class UserDto
    {
        public User User { get; set; }
        public IList<Rank> Ranks { get; set; }
     }
}
