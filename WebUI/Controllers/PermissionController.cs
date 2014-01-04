using System;
using BusinessObjects;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataObjects.EntityFramework;
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

        public ActionResult Edit()
        {
            return View();
        }

        public enum GivePermissionStatus
        {
            Fail, Success, AlreadyHave, NoAccount
        }
        public async Task<GivePermissionStatus> GivePermission(string email, string roleName, UserViewModel user)
        {
            //todo need email add validation

            var returnStatus =  GivePermissionStatus.Fail;
            var userId = user.UserId;

            var role = await _efUserPermissionRepository.GetRoleByName(roleName);
            if (role != null)
            {
                var actAsUser = await _efUserPermissionRepository.GetUserByEmailAsync(email);
                if (actAsUser == null)
                {
                    returnStatus = GivePermissionStatus.NoAccount;
                }
                else
                {
                    var roleId = role.Id;
                    var actAsUserId = actAsUser.Id;
                    var userPermision =
                        await _efUserPermissionRepository.GetUserPermissionAsync(userId, actAsUserId, roleId);
                    if (userPermision != null)
                    {
                        return GivePermissionStatus.AlreadyHave;
                    }

                    var vm = new UserPermission
                    {
                        Id = Guid.NewGuid().ToString(),
                        User_Id = userId,
                        ActAsUser_Id = actAsUserId,
                        Role_ID = roleId
                    };

                    _efUserPermissionRepository.Insert(vm);
                    await _efUserPermissionRepository.SaveChangesAsync();
                    returnStatus = GivePermissionStatus.Success;
                }
            }
            return returnStatus;
        }

        public async Task<bool> RemovePermission(string permissionId, UserViewModel user)
        {
            var permission = await _efUserPermissionRepository.GetUserPermissionSecureAsync(user.UserId, permissionId);
            if (permission != null)
            {
                await _efUserPermissionRepository.DeleteAsync(permission);
                return true;
            }
            return false;
        }
    }
}