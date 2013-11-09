using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace LMate.WebUI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public new IDbSet<ApplicationRole> Roles { get; set; }

        public ApplicationDbContext() : base("DefaultConnection") { }
    }

    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }


    //public class ApplicationDbContext : IdentityDbContext
    //{
    //    public new IDbSet<ApplicationRole> Roles { get; set; }
    //    public new IDbSet<ApplicationUser> Users { get; set; }

    //    public ApplicationDbContext() : base("DefaultConnection") { }


    //}
}