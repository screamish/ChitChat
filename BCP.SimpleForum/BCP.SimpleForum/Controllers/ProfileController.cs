using BCP.SimpleForum.Models;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCP.SimpleForum.Controllers
{
    public class ProfileController : Controller
    {
        private IDocumentSession _session;

        public ProfileController(IDocumentSession session)
        {
            _session = session;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var user = _session.Load<User>(User.Identity.Name);

            return View(user);
        }
    }
}
