using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Models
{
    public class Rank : BaseClass
    {
        public string Name { get; set; }
        [ForeignKey("RankId")]
        public virtual ICollection<UserRank> UserRanks { get; set; }
    }
}
