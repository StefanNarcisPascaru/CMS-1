using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CMS.Domain.Models
{
    public class User :BaseClass //: IdentityUser<Guid>
    {
        //public User()
        //{
        //    Id = Guid.NewGuid();
        //}
        //[Key]
        //public  Guid Id { get; set; }
        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
        public string UserName { get; set; }
        public string CompleteName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<UserRank> UserRanks { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
