using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CMS.WebUI.ViewModels.Resource;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;

namespace CMS.WebUI.Controllers
{
    public class ResourceController : Controller
    {
        public IActionResult Resources(string name=null, string subject=null)
        {
            IList<ResourceDto> resources = new List<ResourceDto>
            {
                 new ResourceDto
                {
                    Id=Guid.Parse("36AD59A2-FE81-445B-97F6-31FD92674DE5"),
                    Name = "Cum sa scrii frumos",
                    Subject = "caligrafie",
                    Type = "pdf",
                   // File = new FormFile(new FileStream(".//AdminController",FileMode.OpenOrCreate)
                    //,1024,512, "nume", "AdminController")
                },
                 new ResourceDto
                {
                    Id=Guid.Parse("36AD59A2-FE81-445B-97F6-31FD92674DE5"),
                    Name = "ChatBot",
                    Subject = "Inteligenta artificiala",
                    Type = "pptx",
                    //File = new FileStream("./ProfessorController",FileMode.Open)
                },
                  new ResourceDto
                {
                    Id=Guid.Parse("36AD59A2-FE81-445B-97F6-31FD92674DE5"),
                    Name = "entity framework",
                    Subject = "Introducere in .net",
                    Type = "pdf",
                    //File = new FileStream("./ProfessorController",FileMode.Open)
                }
            };

            if(name != null)
                resources=resources.Where(r => r.Name.Contains(name)).ToList();
            if (subject != null)
                resources = resources.Where(r => r.Subject.Contains(subject)).ToList();

            return View(resources);
        }
    }
}
