using System;

namespace CMS.Domain.Models
{
    public class UserRank
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid RankId { get; set; }
        public virtual Rank Rank { get; set; }
    }
}
