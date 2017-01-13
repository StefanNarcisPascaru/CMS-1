using System;

namespace CMS.Domain.Models
{
    public class UserRank : BaseClass
    {
        public UserRank()
        {
        }

        public UserRank(Guid userid, Guid rankId)
        {
            UserId = userid;
            RankId = rankId;
        }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid RankId { get; set; }
        public virtual Rank Rank { get; set; }
    }
}
