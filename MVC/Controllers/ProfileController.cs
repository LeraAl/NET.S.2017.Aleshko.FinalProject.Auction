using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Services;
using MVC.Infrasrtucture.Mappers;
using MVC.Models.Lot;
using MVC.Models.Profile;

namespace MVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ILotService _lotService;
        private readonly IUserService _userService;

        public ProfileController(ILotService lotService, IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
        }

        public ActionResult Index()
        {
            var user = _userService.GetByLogin(User.Identity.Name);
            var userVM = user.ToProfileVM();
            userVM.Roles = _userService.GetUserRoles(user.Id).Select(r => r.Name);
            return View(userVM);
        }

        public ActionResult Lots()
        {
            return View();
        }

        public PartialViewResult GetLots(int? id)
        {
            int userId = _userService.GetByLogin(User.Identity.Name).Id;
            var lots = _lotService.GetUserLots(userId);
            if (id != null && id != 0)
                lots = lots.Where(l => l.CategoryId == id);

            return PartialView("_LotsList", lots.Select(l => l.ToLotShortVM()));
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var model = _userService.GetByLogin(User.Identity.Name).ToProfileEditModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProfileEditModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetByLogin(User.Identity.Name);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                _userService.Update(user);

                return RedirectToAction("Index", "Profile");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetByLogin(User.Identity.Name);
                if (Crypto.VerifyHashedPassword(user.Password, model.OldPassword))
                {
                    _userService.UpdatePassword(user.Id, Crypto.HashPassword(model.NewPassword));
                    return RedirectToAction("Index", "Profile");
                }
                ModelState.AddModelError("OldPassword", "Incorrect password.");
            }
            return View(model);
        }
    }
}