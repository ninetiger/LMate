using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebUI.Models;
using Microsoft.AspNet.Identity;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateDefaultRoles();
        }

        private static async void CreateDefaultRoles()
        {
            using (var repo = new ApplicationDbContext())
            {
                ApplicationRole role;
                //Super
                var roleQuery = repo.Roles.Where(x => x.Name == "Super");
                if (! await roleQuery.AnyAsync())
                {
                    role = new ApplicationRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Super",
                        Description = "Site admin."
                    };
                    repo.Roles.Add(role);
                }

                //Individual
                roleQuery = repo.Roles.Where(x => x.Name == "Individual");
                if (!await roleQuery.AnyAsync())
                {
                    role = new ApplicationRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Individual",
                        Description = "User who doing tax for themself."
                    };
                    repo.Roles.Add(role);
                }

                //Delegate
                roleQuery = repo.Roles.Where(x => x.Name == "Delegate");
                if (!await roleQuery.AnyAsync())
                {
                    role = new ApplicationRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Delegate",
                        Description = "User with authority to look other user's account."
                    };
                    repo.Roles.Add(role);
                }

                await repo.SaveChangesAsync();
            }
        }
    }
}
