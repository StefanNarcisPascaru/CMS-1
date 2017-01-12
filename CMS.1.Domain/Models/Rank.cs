using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CMS.Domain.Models
{
    public class Rank : BaseClass
    {
        public string Name { get; set; }
        [ForeignKey("RankId")]
        [JsonIgnore]
        public virtual ICollection<UserRank> UserRanks { get; set; }
    }
}
