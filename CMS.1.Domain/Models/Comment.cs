using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Models
{
    public class Comment
    {   
        
        public Guid UserId { get; set; }
       
        public virtual User User { get; set; }

        public string message { get; set; }

        public string username { get; set; }

        public string subject { get; set; }

    }
}
