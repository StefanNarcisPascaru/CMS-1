using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize("AdminOnly")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize("Professor")]
        public IActionResult Teacher()
        {
            return View();
        }

        [Authorize("FacultyMember")]
        public IActionResult Student()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
