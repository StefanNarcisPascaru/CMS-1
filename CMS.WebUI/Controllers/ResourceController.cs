using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.WebUI.ViewModels.Resource;
using Microsoft.AspNetCore.Mvc;

namespace CMS.WebUI.Controllers
{
    public class ResourceController:Controller
    {
        public IActionResult Resources()
        {
            IList <ResourceDto> resources=new List<ResourceDto>()
            {
                new ResourceDto() {Id=Guid.Parse("36AD59A2-FE81-445B-97F6-31FD92674DE5"), Name = "Cum sa scrii frumos", Subject = "caligrafie", Type = "pdf"}
            };
            return View(resources);
        }
    }
}
