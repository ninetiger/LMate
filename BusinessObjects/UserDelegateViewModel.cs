using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessObjects
{
    public class UserDelegateViewModel
    {
        public string PermissionId { get; set; }

        public List<SelectListItem> PermissionSelectList { get; set; }
    }
}