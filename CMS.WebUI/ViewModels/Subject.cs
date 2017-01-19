using System.ComponentModel.DataAnnotations;

namespace CMS.WebUI.ViewModels
{
    public class Subject
    {
        [Display(Name = "New Subject name")]
        [Required(ErrorMessage = "Every course needs a name!")]
        public string SubjectName { get; set; }
        [Display(Name = "Teacher assigned to")]
        [Required(ErrorMessage = "Please assign the course to a teacher!")]
        public string TeacherName { get; set; }
    }
}
