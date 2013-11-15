using LMate.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.WebUI.Controllers
{
    public class RoleController : Controller
    {
        public RoleManager<ApplicationRole> RoleManager { get; private set; }

        public RoleController()
            : this(new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext())))
        { }

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            RoleManager = roleManager;
        }

        // GET: /Role/
        public async Task<ActionResult> Index()
        {
            List<ApplicationRole> result;
            using (var repository = new ApplicationDbContext())
            {
                result = await repository.Roles.ToListAsync();
            }
            return View(result);
        }

        // GET: /Role/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var applicationRole = await RoleManager.FindByIdAsync(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(new RoleViewModel(applicationRole));
        }

        // GET: /Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Description")] RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Description = model.Description
                };
                var result = await RoleManager.CreateAsync(role); 
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }

                AddErrors(result);
            }

            return View(model);
        }

        // GET: /Role/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(new RoleViewModel(role));
        }

        // POST: /Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var repository = new ApplicationDbContext())
                {
                    var applicationRole = new ApplicationRole
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description
                    };

                    repository.Entry(applicationRole).State = EntityState.Modified;
                    await repository.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /Role/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {

            using (var repository = new ApplicationDbContext())
            {
                var role = repository.Roles.Find(id);
                repository.Roles.Remove(role);

                await repository.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && RoleManager != null)
            {
                RoleManager.Dispose();
                RoleManager = null;
            }
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
