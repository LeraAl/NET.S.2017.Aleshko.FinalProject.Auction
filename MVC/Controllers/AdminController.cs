using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MVC.Filters;
using MVC.Infrasrtucture.Mappers;
using MVC.Models.Profile;

namespace MVC.Controllers
{
    [CustomAuth(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ILotService _lotService;
        private readonly IUserService _userService;
        private readonly IRateService _rateService;

        public AdminController(ILotService lotService, IUserService userService, IRateService rateService)
        {
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
            _lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        #region Administrator

        
        public ActionResult Users()
        {
            var users = _userService.GetAll().Select(u =>
            {
                var user = u.ToProfileForAdmin();
                user.Roles = _userService.GetUserRoles(u.Id).Select(r => r.Name);
                user.RateCount = _rateService.GetUserRates(u.Id).Count();
                user.LotCount = _lotService.GetUserLots(u.Id).Count();
                return user;
            });

            return View(users);
        }

        public ActionResult UserDetails(int? id)
        {
            if (id == null) return RedirectToAction("PageNotFound", "Error");

            var user = _userService.GetById((int)id).ToProfileVM();
            user.Roles = _userService.GetUserRoles((int) id).Select(r => r.Name).ToList();

            var rolesModel = new RolesEditModel()
            {
                Id = (int) id,
                IsAdmin = user.Roles.Contains("Administrator"),
                IsModerator = user.Roles.Contains("Moderator"),
                IsBanned = user.Roles.Contains("Banned")
            };

            TempData["Roles"] = rolesModel;

            return View(user);
        }

        [HttpPost]
        public ActionResult EditRoles(RolesEditModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.IsAdmin)
                    _userService.AddRoleToUser(model.Id, _userService.GetRoleByName("Administrator"));
                else
                    _userService.DeleteRoleFromUser(model.Id, _userService.GetRoleByName("Administrator"));

                if (model.IsModerator)
                    _userService.AddRoleToUser(model.Id, _userService.GetRoleByName("Moderator"));
                else
                    _userService.DeleteRoleFromUser(model.Id, _userService.GetRoleByName("Moderator"));
                if (model.IsBanned)
                    _userService.AddRoleToUser(model.Id, _userService.GetRoleByName("Banned"));
                else
                    _userService.DeleteRoleFromUser(model.Id, _userService.GetRoleByName("Banned"));
            }

            return RedirectToAction("UserDetails", new { id = model.Id });
        }

        #endregion
        
    }
}