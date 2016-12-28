using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.BussinesInterfaces.ModelLogic;
using CMS.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Authentication;

namespace CMS.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserLogic _userLogic;
        //private readonly SignInManager<User> _loginManager;
//        private readonly RoleManager<Rank> _roleManager;

        public AccountController(IUserLogic userLogic
          //      SignInManager<User> loginManager
            //    RoleManager<Rank> roleManager
            )
        {
            _userLogic = userLogic;
            //_loginManager = loginManager;
            //_roleManager = roleManager;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if (!_userLogic.IsValidUser(user.UserName, user.Password))
            {
                ModelState.AddModelError("LoginFail", "Username și/sau parolă incorecte!");
                return View(user);
            }

            // var result = _loginManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe,false).Result;
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                //new Claim("Rank","Professor")
            };
            var ranks = _userLogic.GetRanks(user.UserName);
            claims.AddRange(ranks.Select(rank => new Claim("Rank", rank)));//foreach prescurtat cu linq
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims,"Cookie"));
            await HttpContext.Authentication.SignInAsync("Cookies", principal,
                new AuthenticationProperties()
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(25),
                    IsPersistent = user.RememberMe
                });
            return RedirectToAction("About", "Home");
        }

        public IActionResult Forbidden()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return RedirectToAction("Index","Home");
        }
    }
}
