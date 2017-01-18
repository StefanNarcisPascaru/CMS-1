using System;
using System.Collections.Generic;
using CMS.BussinesInterfaces.ModelLogic;
using CMS.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.WebUI.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IRankLogic _rankLogic;
        private readonly IUserLogic _userLogic;

        public AdminController(IUserLogic userLogic, IRankLogic rankLogic)
        {
            _userLogic = userLogic;
            _rankLogic = rankLogic;
        }

        public IActionResult ManageUsers()
        {
            var detailedUsers = new List<UserDto>();
            var users = _userLogic.GetUsers();
            foreach (var user in users)
            {
                detailedUsers.Add(new UserDto() {User = user, Ranks = _rankLogic.GetUserRanks(user.Id)});
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
