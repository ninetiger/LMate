using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
using WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        public UserController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {}

        public UserController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            userManager.PasswordValidator = new MinimumLengthValidator(3);
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        // GET: /User/
        public async Task<ActionResult> Index()
        {
            List<ApplicationUser> result;
            using (var repo = new ApplicationDbContext())
            {
                result = await repo.Users.ToListAsync();
            }
            return View(result);
        }

        // GET: /User/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: /User/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name,IRDNumber,Address")] TaxUser taxuser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TaxUsers.Add(taxuser);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(taxuser);
        //}

        // GET: /User/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user  == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            //[Bind(Include = "Id,Name,IRDNumber,Address")] 
            ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                using (var repo = new ApplicationDbContext())
                {
                    //var entityAspNetUserDao = new EntityAspNetUserDao(new LMateEntities());
                    //var userDao = await entityAspNetUserDao.GetByIDAsync(user.Id);
                    //userDao.AspNetUserRoles.Add(user.Roles[0]);
                    repo.Entry(user).State = EntityState.Modified;
                    await repo.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //// GET: /User/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TaxUser taxuser = await db.TaxUsers.FindAsync(id);
        //    if (taxuser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(taxuser);
        //}

        //// POST: /User/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    TaxUser taxuser = await db.TaxUsers.FindAsync(id);
        //    db.TaxUsers.Remove(taxuser);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }
    }
}
