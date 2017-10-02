using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfaces.Services;
using MVC.Models.Account;
using MVC.Providers;

namespace MVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        #region Register

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (_userService.GetByLogin(model.Login) != null)
            {
                ModelState.AddModelError("", "User with this login already registered.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(model);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Error", "Error");
            }
            return View(model);
        }

        #endregion

        #region LogIn LogOut

        [Authorize]
        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel model, string returnUrl)
        {
            bool userExist = ((CustomMembershipProvider)Membership.Provider).ValidateUser(model.Login, model.Password);
            if (!userExist)
            {
                ModelState.AddModelError("", "Incorrect login or password");
                return View(model);
            }

            int userId = _userService.GetByLogin(model.Login).Id;
            if (_userService.GetUserRoles(userId).Any(r => r.Name.Equals("Banned")))
            {
                ModelState.AddModelError("", "User is banned.");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);

            if (Url.IsLocalUrl(returnUrl))
                Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult LogOut(string returnUrl)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}