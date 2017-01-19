using System;
using System.Collections.Generic;
using System.Linq;
using CMS.BussinesInterfaces.ModelLogic;
using CMS.WebUI.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.WebUI.Controllers
{
    [Authorize("AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IRankLogic _rankLogic;
        private readonly IUserLogic _userLogic;

        public AdminController(IUserLogic userLogic, IRankLogic rankLogic)
        {
            _userLogic = userLogic;
            _rankLogic = rankLogic;
        }

        public IActionResult ManageUsers(string completeName = null, string username = null)
        {
            var detailedUsers = new List<UserDto>();
            var users = _userLogic.GetUsers();
            if (completeName != null)
                users = users.Where(u => u.CompleteName.Contains(completeName)).ToList();
            if (username != null)
                users = users.Where(u => u.UserName.Contains(username)).ToList();
            foreach (var user in users)
            {
                detailedUsers.Add(new UserDto() { User = user, Ranks = _rankLogic.GetUserRanks(user.Id) });
            }
            return View(detailedUsers);
        }

        public IActionResult EditUser(Guid id)
        {
            var user = new UserDto
            {
                User = _userLogic.GetUser(id),
                Ranks = _rankLogic.GetUserRanks(id)
            };
            return View(user);
        }

        [HttpPost]
        public IActionResult SaveChangesOnUser(UserDto userDto)
        {
            _userLogic.Update(userDto.User, userDto.Ranks);

            return RedirectToAction("ManageUsers");
        }
    }
}
