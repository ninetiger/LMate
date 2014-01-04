using BusinessObjects;
using DataObjects.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermissionViewModel>
    {
        Task<UserPermission> GetUserPermissionSecureAsync(string userId, string permissionId);

        IEnumerable<UserPermission> GetAllByUserId(string userId);
    }
}
