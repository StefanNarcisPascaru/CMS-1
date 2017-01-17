using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CMS.Domain.Models
{
    public class User : BaseClass
    {
        public string UserName { get; set; }
        public string CompleteName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual ICollection<UserRank> UserRanks { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<Comment> Comments { get; set; }
    }

    
}
