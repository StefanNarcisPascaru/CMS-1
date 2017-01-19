using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.BussinesInterfaces.ModelLogic;
using CMS.Domain;
using CMS.Domain.Models;
using CMS.WebUI.ViewModels;
using CMS.WebUI.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Authentication;

namespace CMS.WebUI.Controllers
{
    //[RequireHttps]
    public class AccountController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly IEnumerable<Rank> _ranks;
        public AccountController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
            using (var context=new CmsContext())
            {
                _ranks = new List<Rank>(context.Ranks.ToList());
            }
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Index", "Home");
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

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
            };
            var ranks = _userLogic.GetRanks(user.UserName);

            claims.AddRange(ranks.Select(rank => new Claim("Rank", rank)));//foreach prescurtat cu linq
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie"));

            await HttpContext.Authentication.SignInAsync("Cookies", principal,
                new AuthenticationProperties()
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(25),
                    IsPersistent = false
                });

            if(ranks.ElementAt(0).Equals("FacultyMember"))
            {
                return RedirectToAction("Student", "Home");
            }
            if (ranks.ElementAt(0).Equals("Professor"))
            {
                return RedirectToAction("Courses", "Professor");
            }
            if (ranks.ElementAt(0).Equals("Admin"))
            {
                return RedirectToAction("Admin", "Home");
            }
            return View(user);
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
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewBag.Ranks = _ranks.Select(p=>p.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserRegister model,List<string> rankList,  string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                CompleteName = model.CompleteName,
                Password = model.Password,
                Email = model.Email,
                UserRanks = new List<UserRank>()
            };

            var rankId=_ranks.First(r=>r.Name.Equals(model.Rank.ToString()));
            user.UserRanks.Add(new UserRank
            {
                UserId = user.Id,
                RankId = rankId.Id
            });

            using (var context = new CmsContext())
            {
                context.Users.Add(user);

                context.SaveChanges();
            }
            return RedirectToAction("Login","Account");
        }
    }
}
