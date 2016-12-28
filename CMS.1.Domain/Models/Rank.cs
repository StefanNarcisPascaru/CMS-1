using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CMS.Domain.Models
{
    public class Rank :BaseClass//: IdentityRole<Guid>
    {
        //public Rank()
        //{
        //    Id = Guid.NewGuid();
        //}
        //[Key]
        //public  Guid Id { get; set; }
        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
        public string Name { get; set; }
        [ForeignKey("RankId")]
        public virtual ICollection<UserRank> UserRanks { get; set; }
    }
}
