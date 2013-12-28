using System;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebUI.Models;

namespace WebUI.Binders
{
    public class UserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }

            IPrincipal p = controllerContext.HttpContext.User;

            var userViewModel = new UserViewModel()
            {
                UserId = p.Identity.GetUserId()
            };
            return userViewModel;
        }
    }
}