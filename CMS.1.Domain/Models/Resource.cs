 using System;

namespace CMS.Domain.Models
{
    public class Resource
    {   

        

        public Guid SubjectNo { get; set; }

        public virtual Subject Subject { get; set; }

        public string type { get; set; }

        public string path { get; set; }
    


    }
}
