using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Models
{
    public class Subject : BaseClass
    {
        
        public string subjectName { get; set; }

        public string teacherName { get; set; }

      //  [ForeignKey("SubjectId")]
       // public virtual ICollection<Resource> Resources { get; set; }
    }
}
