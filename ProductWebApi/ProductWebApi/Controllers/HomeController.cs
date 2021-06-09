using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductWebApi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [Authorize(Roles="User,Admin")]
        public ActionResult UserArea()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        public ActionResult AdminArea()
        {
            return View();
        }
    }
}