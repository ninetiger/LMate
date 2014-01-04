using DataObjects.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        Task<UserPermission> GetUserPermissionSecureAsync(string userId, string permissionId);

        Task<UserPermission> GetUserPermissionAsync(string userId, string actAsUserId, string roleId);

        IEnumerable<UserPermission> GetAllByUserId(string userId);

        Task<AspNetUser> GetUserByEmailAsync(string email);

        Task<AspNetRole> GetRoleByName(string roleName);

    }
}
