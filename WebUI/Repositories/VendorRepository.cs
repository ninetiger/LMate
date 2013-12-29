using BusinessObjects;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
using DataObjects.EntityFramework.ModelMapper;
using DataObjects.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly LMateEntities _context;
        private readonly EntityVendorDao _entityVendorDao;

        public VendorRepository(LMateEntities context)
        {
            _context = context;
            _entityVendorDao = new EntityVendorDao(_context);
        }

        #region IRepository

        public async Task<IEnumerable<VendorViewModel>> GetAllByUserIdAsync(string userId)
        {
            //get both generic and user specific vendors
            var list = await _entityVendorDao.GetAsync(x => x.User_Id == null || x.User_Id == userId);
            var vmQuery = list.Select(Mapper.Map);
            return vmQuery;
        }

        public void Insert(VendorViewModel entityToInsert)
        {
            var vendor = Mapper.Map(entityToInsert);
            _entityVendorDao.Insert(vendor);
        }

        public async Task Update(VendorViewModel entityToUpdate)
        {
            var vendor = await _entityVendorDao.GetByIDAsync(entityToUpdate.Id);
            vendor.Name = entityToUpdate.VendorName;
            vendor.User_Id = entityToUpdate.UserId;
            vendor.Version = entityToUpdate.Version.AsByteArray(); //todo need to understand how to use it

            _entityVendorDao.Update(vendor);
        }


        /// <summary>
        /// Delete a vendor
        /// </summary>
        public async Task DeleteAsync(VendorViewModel entityToDelete)
        {
            var vendor = await _entityVendorDao.GetByIDAsync(entityToDelete.Id);
            _entityVendorDao.Delete(vendor);
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

        #region IVendorRepository

        public async Task<Vendor> GetVednor(string vendorName)
        {
            var vendorEnumerable = await _entityVendorDao.GetAsync(x => x.Name == vendorName);
            var vendor = vendorEnumerable.SingleOrDefault();
            return vendor;
        }
        public async Task<List<List<string>>> SearchVendorName(string searchString)
        {
            IEnumerable<Vendor> list;
            if (string.IsNullOrEmpty(searchString))
            {
                list = await _entityVendorDao.GetAsync();
            }
            else
            {
                list = await _entityVendorDao.GetAsync(x => x.Name.ToLower().Contains(searchString.ToLower()));
            }

            return list.ToArray().Select(item => new List<string>()
            {
                item.Id.ToString(CultureInfo.InvariantCulture), 
                item.Name
            }).ToList();
        }
        public void InsertVendor(Vendor vendor)
        {
            _entityVendorDao.Insert(vendor);
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
