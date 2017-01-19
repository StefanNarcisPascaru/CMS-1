using Microsoft.AspNetCore.Http;

namespace CMS.WebUI.ViewModels
{
    public class Resource
    {
        public string Type { get; set; }

        public string CourseName { get; set; }

        public IFormFile File { get; set; }
    }

    public enum ResourceType
    {
        Course,
        Demo
    }
}
