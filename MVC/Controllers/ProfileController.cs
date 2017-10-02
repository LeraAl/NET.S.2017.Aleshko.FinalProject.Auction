using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MVC.Infrasrtucture.Mappers;
using MVC.Models.Profile;

namespace MVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILotService _lotService;
        private readonly IUserService _userService;
        private readonly IRateService _rateService;

        public ProfileController(ILotService lotService, IUserService userService, IRateService rateService)
        {
            _rateService = rateService ?? throw new ArgumentNullException(nameof(rateService));
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

        public ActionResult Lots()
        {
            return View();
        }

        public ActionResult BoughtLots()
        {
            int userId = _userService.GetByLogin(User.Identity.Name).Id;
            var lots = _lotService.GetByState("Sold")
                .Where(l => _rateService.GetLotLastRate(l.Id).UserId == userId)
                .Select(l => l.ToLotShortVM());

            return View("_LotsList", lots);
        }

        public ActionResult SoldLots()
        {
            int userId = _userService.GetByLogin(User.Identity.Name).Id;
            var lots = _lotService.GetUserLots(userId).Where(l => l.State.Equals("Sold")).Select(l => l.ToLotShortVM());

            return View("_LotsList", lots);
        }

        public ActionResult Rates()
        {
            int userId = _userService.GetByLogin(User.Identity.Name).Id;
            var rates = _rateService.GetUserRates(userId).OrderBy(r => r.Datetime).Select(r => r.ToRateVM());
            return View(rates);
        }

    }
}