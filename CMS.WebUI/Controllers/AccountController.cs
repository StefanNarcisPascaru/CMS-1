using CMS.BussinesInterfaces.ModelLogic;
using CMS.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserLogic _userLogic;

        public AccountController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLogin user)
        {
            if (!_userLogic.IsValidUser(user.UserName, user.Password))
            {
                ModelState.AddModelError("LoginFail", "Username și/sau parolă incorecte!");
                return View(user);
            }
            
            return RedirectToAction("About","Home");
        }
    }
}
