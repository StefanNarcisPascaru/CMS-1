using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Models
{
    public class User : BaseClass
    {
        public string UserName { get; set; }
        public string CompleteName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<UserRank> UserRanks { get; set; }
    }
}
