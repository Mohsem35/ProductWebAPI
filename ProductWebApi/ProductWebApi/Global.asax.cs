using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProductWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //using(ApplicationDbContext db = new ApplicationDbContext())
        protected void Application_Start()
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                if(db.Roles.Count() == 0)
                {
                    var role1 = new UserRole();
                    var role2 = new UserRole();

                    db.Roles.Add(role1);
                    db.Roles.Add(role2);

                    role1.RoleName = "User";
                    role2.RoleName = "Admin";
                    db.SaveChanges();
                }
            }

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
