using BusinessObjects;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
using DataObjects.EntityFramework.ModelMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly LMateEntities _context;
        private readonly EntityAccountTypeDao _entityAccountTypeDao;
        private readonly EntityReceiptDao _entityReceiptDao;
        private readonly EntityCurrencyDao _entityCurrencyDao;
        private readonly EntityReceiptImageDao _entityReceiptImageDao;
        private readonly EntityReceiptCategoryDao _entityReceiptCategoryDao;
        private readonly EntityVendorDao _entityVendorDao;

        public ReceiptRepository(LMateEntities context)
        {
            _context = context;
            _entityAccountTypeDao = new EntityAccountTypeDao(_context);
            _entityReceiptDao = new EntityReceiptDao(_context);
            _entityCurrencyDao = new EntityCurrencyDao(_context);
            _entityReceiptImageDao = new EntityReceiptImageDao(_context);
            _entityReceiptCategoryDao = new EntityReceiptCategoryDao(_context);
            _entityVendorDao = new EntityVendorDao(_context);
        }

        #region IRepository

        public async Task<IEnumerable<ReceiptViewModel>> GetAllByUserIdAsync(string userId)
        {
            var list = await _entityReceiptDao.GetAsync(x => x.User_Id == userId);
            var vmQuery = list.Select(Mapper.Map);
            return vmQuery;
        }

        public void Insert(ReceiptViewModel entityToInsert)
        {
            var receipt = Mapper.Map(entityToInsert);
            receipt.ReceiptStatus_Id = 1; //new record default to 1 //todo need te move this line to controler level later

            _entityReceiptDao.Insert(receipt);
        }

        public async Task Update(ReceiptViewModel entityToUpdate)
        {
            var receipt = await GetReceiptSecure(entityToUpdate.Id, entityToUpdate.UserId);
            receipt.Description = entityToUpdate.Description;
            receipt.Reference = entityToUpdate.Reference;
            receipt.IsBulk = entityToUpdate.IsBulk;
            receipt.PurchaseDate = entityToUpdate.PurchaseDate;
            receipt.Price = entityToUpdate.Price;
            receipt.IsIncludeTax = entityToUpdate.IsIncludeTax;
            receipt.IsTaxExclusive = entityToUpdate.IsTaxExclusive;
            receipt.GstRate = entityToUpdate.GstRate;
            receipt.Tax = entityToUpdate.Tax;
            receipt.Note = entityToUpdate.Note;
            receipt.ReceiptCategory_Id = entityToUpdate.ReceiptCategoryId;
            receipt.Currency_Id = entityToUpdate.CurrencyId;
            receipt.AccountType_Id = entityToUpdate.AccountTypeId;
            //receipt.Version = entityToUpdate.Version.AsByteArray(); //todo need to understand how to use it

            if (receipt.Vendor_Id != entityToUpdate.VendorId)
            {
                receipt.Vendor_Id = entityToUpdate.VendorId;
                receipt.Vendor = Mapper.Map(entityToUpdate.Vendor);
            }

            _entityReceiptDao.Update(receipt);
        }

        /// <summary>
        /// Delete a receipt
        /// </summary>
        public async Task DeleteAsync(ReceiptViewModel entityToDelete)
        {
            var receipt = await GetReceiptSecure(entityToDelete.Id, entityToDelete.UserId);
            receipt.ReceiptImages.Clear();
            _entityReceiptDao.Delete(receipt);
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

        #region IReceiptRepository

        /// <summary>
        /// Get a receipt by its id and make sure the receipt does belong to the user
        /// </summary>
        public async Task<Receipt> GetReceiptSecure(int receiptId, string userId)
        {
            var receipt = await _entityReceiptDao.GetByIDAsync(receiptId);
            if (receipt.User_Id.Equals(userId)) //only if the receipt belong to the user
            {
                return receipt;
            }

            //todo seems that the exception is send back to the client that .ajax's error func is raised
            //need to log and see how to handel the exception at the server side
            throw new Exception("User " + userId + "is tring to access a receipt does not belong to him/her");
        }

        public async Task<IEnumerable<ReceiptBriefViewModel>> GetReceiptBriefsByUserIdAsync(string userId)
        {
            var list = await _entityReceiptDao.GetAsync(x => x.User_Id == userId);
            var vmQuery = list.Select(x => new ReceiptBriefViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Vendor = x.Vendor != null ? x.Vendor.Name : string.Empty,
                PurchaseDate = x.PurchaseDate,
                Price = x.Price,
                HasImage = x.ReceiptImages.Any() ? "Yes" : "No",
                DateEntered = x.DateEntered,
                Status = x.ReceiptStatus.Status
            });

            return vmQuery;
        }

        public async Task<ReceiptEditViewModel> GetReceiptForEditAsync(int receiptId, string userId)
        {
            ReceiptViewModel receiptVm;
            if (receiptId == 0)
            {
                receiptVm = new ReceiptViewModel { UserId = userId };
            }
            else
            {
                var receipt = await GetReceiptSecure(receiptId, userId);
                receiptVm = Mapper.Map(receipt);
            }

            return await GetReceiptForEditViewModelAsync(receiptVm);
        }

        public async Task<ReceiptEditViewModel> GetReceiptForEditViewModelAsync(ReceiptViewModel receiptVm)
        {
            var accountTypeQuery = await _entityAccountTypeDao.GetAsync(x => x.User_Id == receiptVm.UserId || x.User_Id == null);
            var accountTypeSelectList = accountTypeQuery.Select(x => new SelectListItem
            {
                Selected = x.Id == (receiptVm.AccountTypeId ?? -1),
                Text = x.Type,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            });

            var currencyQuery = await _entityCurrencyDao.GetAsync();
            var currenciesSelectList = currencyQuery.Select(x => new SelectListItem
            {
                Selected = x.Id == receiptVm.CurrencyId,
                Text = x.Name,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            });

            var categoryQuery =
                await _entityReceiptCategoryDao.GetAsync(x => x.User_Id == receiptVm.UserId || x.User_Id == null);
            var categorySelectList = categoryQuery.Select(x => new SelectListItem
            {
                Selected = x.Id == (receiptVm.ReceiptCategoryId ?? -1),
                Text = x.Type,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            });
            //var multiSelectList = new MultiSelectList(categoryQuery, "Id", "Type");

            return new ReceiptEditViewModel
            {
                ReceiptViewModel = receiptVm,
                AccountTypeSelectList = accountTypeSelectList,
                CurrencySelectList = currenciesSelectList,
                CategorySelectList = categorySelectList
            };
        }

        public async Task InsertImage(ReceiptImage image, int receiptId, string userId)
        {
            var receipt = await GetReceiptSecure(receiptId, userId);
            image.Receipts.Add(receipt);
            _entityReceiptImageDao.Insert(image);
        }

        public async Task<ReceiptImage> GetImageSecure(int imageId, string userId)
        {
            var image = await _entityReceiptImageDao.GetAsync(x => x.Id == imageId);

            var receiptImages = image as ReceiptImage[] ?? image.ToArray();
            if (receiptImages.Count() > 1)
                throw new Exception("Duplicated receiptImage id!!!");

            if (receiptImages.Count() == 1)
            {
                var receiptImage = receiptImages.ToArray()[0];
                if (receiptImage.User_Id.Equals(userId))
                {
                    return receiptImage;
                }

                throw new Exception(string.Format("User {0} is tring to access a receipt does not belong to him/her", userId));
            }

            return null;
        }

        public async Task<string> GetImageAddrsByReceiptId(int receiptId, string userId)
        {
            var receipt = await GetReceiptSecure(receiptId, userId);

            var sb = new StringBuilder();
            foreach (var image in receipt.ReceiptImages)
            {
                sb.Append(image.Id.ToString(CultureInfo.InvariantCulture) + ',' + image.Description + ';');
            }
            return sb.ToString();
        }

        public async Task DetachAnImageFromReceipt(int imageId, int receiptId, string userId)
        {
            var receipt = await GetReceiptSecure(receiptId, userId);
            var image = receipt.ReceiptImages.SingleOrDefault(x => x.Id == imageId);
            if (image != null)
            {
                receipt.ReceiptImages.Remove(image);
                await SaveChangesAsync();
            }
        }

        #region Vendor
        public async Task<Vendor> GetVednorSecure(int vendorId, string userId)
        {
            var vendor = await _entityVendorDao.GetByIDAsync(vendorId);
            return VendorSeureCheck(vendor, userId);
        }

        public async Task<Vendor> GetVednorSecure(string vendorName, string userId)
        {
            var vendorEnumerable = await _entityVendorDao.GetAsync(x => x.Name == vendorName);
            var vendor = vendorEnumerable.SingleOrDefault();
            return VendorSeureCheck(vendor, userId);
        }

        private static Vendor VendorSeureCheck(Vendor vendor, string userId)
        {
            if (vendor != null && (vendor.User_Id == null || vendor.User_Id.Equals(userId)))
            {
                return vendor;
            }

            return null;
        }

        public async Task<string[]> SearchVendorNameSecure(string searchString, string userId)
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
            return list.Where(x => x.User_Id == null || x.User_Id.Equals(userId)).Select(item => item.Name).ToArray();
        }

        //public void InsertVendor(Vendor vendor)
        //{
        //    _entityVendorDao.Insert(vendor);
        //}
        #endregion

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
