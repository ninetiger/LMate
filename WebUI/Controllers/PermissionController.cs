using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessObjects;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class PermissionController : Controller
    {
        private readonly EntityUserPermission _entityUserPermissionDao;

        public PermissionController(LMateEntities context)
        {
            _entityUserPermissionDao = new EntityUserPermission(context);

        }
        //
        // GET: /Permission/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PermissionDropDown(UserViewModel user)
        {
            var vm = new UserDelegateViewModel
            {
                PermissionSelectList = new List<SelectListItem> {
                                                        new SelectListItem{
                                                            Selected = true,
                                                            Text = User.Identity.Name,
                                                            Value = string.Empty
                                                        } }
            };

            if (Session["PermissionId"] == null || string.Empty.Equals(Session["PermissionId"].ToString()))
            {
                Session["PermissionId"] = string.Empty;
                vm.PermissionId = string.Empty;
            }
            else
            {
                vm.PermissionId = Session["PermissionId"].ToString();
            }

            var query = _entityUserPermissionDao.Get(x => x.User_Id == user.UserId);
            vm.PermissionSelectList.AddRange(query.Select(x => new SelectListItem
            {
                Text = x.AspNetUser1.UserName + " as " + x.AspNetRole.Name,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList());

            return PartialView(vm);
        }

        public async Task<bool> SetCurrentUser(string permissionId, string returnUrl, UserViewModel user)
        {
            bool hasPermission = await CheckUserCredential(user.UserId, permissionId);
            if (hasPermission)
            {
                Session["PermissionId"] = permissionId;
            }
            else
            {
                Session["PermissionId"] = string.Empty;
            }
            return hasPermission;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }

        private async Task<bool> CheckUserCredential(string userId, string permissionId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(permissionId))
            {
                var permissionList =
                    await _entityUserPermissionDao.GetAsync(x => x.User_Id == userId
                                                             && x.Id == permissionId);

                if (permissionList.Count() == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}