using BusinessObjects;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
using DataObjects.EntityFramework.ModelMapper;
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

        public UserPermissionRepository(LMateEntities context)
        {
            _context = context;
            _entityUserPermission = new EntityUserPermission(_context);
        }

        #region IRepository

        public async Task<IEnumerable<UserPermissionViewModel>> GetAllByUserIdAsync(string userId)
        {
            //get both generic and user specific vendors
            var list = await _entityUserPermission.GetAsync(x => x.User_Id == userId);
            var vmQuery = list.Select(Mapper.Map);
            return vmQuery;
        }

        public void Insert(UserPermissionViewModel entityToInsert)
        {
            var userPermission = Mapper.Map(entityToInsert);
            _entityUserPermission.Insert(userPermission);
        }

        public async Task Update(UserPermissionViewModel entityToUpdate)
        {
            var userPermission = await _entityUserPermission.GetByIDAsync(entityToUpdate.Id);
            userPermission.User_Id = entityToUpdate.UserId;
            userPermission.ActAsUser_Id = entityToUpdate.ActAsUserId;
            userPermission.Role_ID = entityToUpdate.RoleID;
            //userPermission.Version = entityToUpdate.Version.AsByteArray(); //todo need to understand how to use it

            _entityUserPermission.Update(userPermission);
        }


        /// <summary>
        /// Delete a userPermission
        /// </summary>
        public async Task DeleteAsync(UserPermissionViewModel entityToDelete)
        {
            var userPermission = await _entityUserPermission.GetByIDAsync(entityToDelete.Id);
            _entityUserPermission.Delete(userPermission);
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
        public async Task<UserPermission> GetUserPermissionSecure(string userId, string permissionId)
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
