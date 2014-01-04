using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly LMateEntities _context;
        private readonly EntityUserPermission _entityUserPermission;
        private readonly EntityAspNetUserDao _entityAspNetUserDao;
        private readonly EntityAspNetRoleDao _entityAspNetRoleDao;

        public UserPermissionRepository(LMateEntities context)
        {
            _context = context;
            _entityUserPermission = new EntityUserPermission(_context);
            _entityAspNetUserDao = new EntityAspNetUserDao(_context);
            _entityAspNetRoleDao = new EntityAspNetRoleDao(_context);
        }

        #region IRepository

        public async Task<IEnumerable<UserPermission>> GetAllByUserIdAsync(string userId)
        {
            var list = await _entityUserPermission.GetAsync(x => x.User_Id == userId);
            return list;
        }

        public void Insert(UserPermission entityToInsert)
        {
            _entityUserPermission.Insert(entityToInsert);
        }

        public async Task Update(UserPermission entityToUpdate)
        {
            var userPermission = await _entityUserPermission.GetByIDAsync(entityToUpdate.Id);
            userPermission.User_Id = entityToUpdate.User_Id;
            userPermission.ActAsUser_Id = entityToUpdate.ActAsUser_Id;
            userPermission.Role_ID = entityToUpdate.Role_ID;
            //userPermission.Version = entityToUpdate.Version.AsByteArray(); //todo need to understand how to use it

            _entityUserPermission.Update(userPermission);
        }


        /// <summary>
        /// Delete a userPermission
        /// </summary>
        public async Task DeleteAsync(UserPermission entityToDelete)
        {
            await _entityUserPermission.DeleteAsync(entityToDelete);
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        #endregion

        #region IUserPermissionRepository
        public async Task<UserPermission> GetUserPermissionSecureAsync(string userId, string permissionId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(permissionId))
            {
                var permissionList =
                    await _entityUserPermission.GetAsync(x => x.User_Id == userId
                                                             && x.Id == permissionId);

                if (permissionList.Count() == 1)
                {
                    return permissionList.Single();
                }
            }

            return null;
        }

        public async Task<UserPermission> GetUserPermissionAsync(string userId, string actAsUserId, string roleId)
        {
            var list = await _entityUserPermission.GetAsync(x => x.User_Id == userId && x.ActAsUser_Id == actAsUserId && x.Role_ID == roleId);
            return list.SingleOrDefault();
        }

        public IEnumerable<UserPermission> GetAllByUserId(string userId)
        {
            return _entityUserPermission.Get(x => x.User_Id == userId);
        }

        public async Task<AspNetUser> GetUserByEmailAsync(string email)
        {
            var user = await _entityAspNetUserDao.GetAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            return user.SingleOrDefault();
        }

        public async Task<AspNetRole> GetRoleByName(string roleName)
        {
            var role = await _entityAspNetRoleDao.GetAsync(x => x.Name == roleName);
            return role.SingleOrDefault();
        }


        #endregion


        #region IDispose
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
