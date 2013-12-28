﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebUI.App_Start;
using WebUI.Binders;
using WebUI.Models;

namespace WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            //ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            ModelBinders.Binders.Add(typeof(UserViewModel), new UserModelBinder());

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
