using BusinessObjects;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.Repositories;

namespace WebUI.Controllers
{
    public class PermissionController : Controller
    {
        private readonly IUserPermissionRepository _efUserPermissionRepository;

        public PermissionController(IUserPermissionRepository efUserPermissionRepository)
        {
            _efUserPermissionRepository = efUserPermissionRepository;
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

            //todo can not use async as this is a partialView ???
            var query = _efUserPermissionRepository.GetAllByUserId(user.UserId);
            vm.PermissionSelectList.AddRange(query.Select(x => new SelectListItem
            {
                Text = x.AspNetUser1.UserName + " as " + x.AspNetRole.Name,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList());

            return PartialView(vm);
        }

        public async Task<bool> SetCurrentUser(string permissionId, string returnUrl, UserViewModel user)
        {
            var userPermision = await _efUserPermissionRepository.GetUserPermissionSecureAsync(user.UserId, permissionId);
            bool hasPermission = userPermision != null;
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
    }
}