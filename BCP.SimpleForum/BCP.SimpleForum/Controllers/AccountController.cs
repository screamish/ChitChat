using BCP.SimpleForum.Models;
using FlexProviders.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCP.SimpleForum.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IFlexMembershipProvider _membershipProvider;
        private readonly IFlexOAuthProvider _oAuthProvider;
        private readonly ISecurityEncoder _encoder = new DefaultSecurityEncoder();

        public AccountController(IFlexMembershipProvider membership, IFlexOAuthProvider oauth)
        {
            _membershipProvider = membership;
            _oAuthProvider = oauth;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SimpleLogin()
        {
            return PartialView(new LoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && _membershipProvider.Login(model.UserName, model.Password, model.RememberMe))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _membershipProvider.Logout();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    var user = new User { Username = model.UserName, Password = model.Password };
                    _membershipProvider.CreateAccount(user);
                    ViewBag.FlashSuccess = "Your account has been created.";
                    return RedirectToAction("Index", "Home");
                }
                catch (FlexMembershipException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private static string ErrorCodeToString(FlexMembershipStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case FlexMembershipStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case FlexMembershipStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case FlexMembershipStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case FlexMembershipStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case FlexMembershipStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case FlexMembershipStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case FlexMembershipStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case FlexMembershipStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case FlexMembershipStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
