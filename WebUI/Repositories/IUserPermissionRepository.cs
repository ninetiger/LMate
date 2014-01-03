using System.Threading.Tasks;
using BusinessObjects;
using DataObjects.EntityFramework;

namespace WebUI.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermissionViewModel>
    {
        Task<UserPermission> GetUserPermissionSecure(string userId, string permissionId);
    }
}
