using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProductWebApi.Controllers
{
    public class LoginController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // GET: Login

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            ModelState.Clear();

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {

            var user = db.Users.Where(w => w.Email == login.Email && w.Password == login.Password).FirstOrDefault();
            if (user != null)
            {
                var TIcket = new FormsAuthenticationTicket(login.Email, true, 3000);
                string Encrypt = FormsAuthentication.Encrypt(TIcket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Encrypt);
                cookie.Expires = DateTime.Now.AddHours(3000);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                if (user.RoleId == 1)
                {
                    return RedirectToAction("UserArea", "Home");
                }
                else
                {
                    return RedirectToAction("AdminArea", "Home");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}