using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CMS.WebUI.ViewModels.Resource
{
    public class ResourceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public IFormFile File { get; set; }

    }

    public enum ResourceType
    {
        Course,
        Demo
    }
}

