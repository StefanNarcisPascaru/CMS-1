using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Models
{
    public class Subject : BaseClass
    {
        
        public string subjectName { get; set; }

        public string teacherName { get; set; }

        [ForeignKey("SubjectNo")]
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
