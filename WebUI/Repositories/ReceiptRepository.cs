using System.Collections.Generic;
using System.Text;
using BusinessObjects;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataObjects.EntityFramework.ModelMapper;
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

        public ReceiptRepository()
        {
            _context = new LMateEntities();
            _entityAccountTypeDao = new EntityAccountTypeDao(_context);
            _entityReceiptDao = new EntityReceiptDao(_context);
            _entityCurrencyDao = new EntityCurrencyDao(_context);
            _entityReceiptImageDao = new EntityReceiptImageDao(_context);
            _entityReceiptCategoryDao = new EntityReceiptCategoryDao(_context);
        }

        public async Task<IEnumerable<ReceiptViewModel>> GetAllByUserIdAsync(string userId)
        {
            var list = await _entityReceiptDao.GetAsync(x => x.User_Id == userId);
            var vmQuery = list.Select(Mapper.Map);
            return vmQuery;
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

        public void Insert(ReceiptViewModel entityToInsert)
        {
            _entityReceiptDao.Insert(Mapper.Map(entityToInsert));
        }

        public async Task Update(ReceiptViewModel entityToUpdate)
        {
            var receipt = await _entityReceiptDao.GetByIDAsync(entityToUpdate.Id);
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
            receipt.Vendor_Id = entityToUpdate.VendorId;
            receipt.ReceiptCategory_Id = entityToUpdate.ReceiptCategoryId;
            receipt.Currency_Id = entityToUpdate.CurrencyId;
            receipt.AccountType_Id = entityToUpdate.AccountTypeId;
            //receipt.Version = entityToUpdate.Version.AsByteArray(); //todo need to understand how to use it
            //receipt.ReceiptImages.Add(entityToUpdate.im);  //todo not sure how to add multi photos yet

            _entityReceiptDao.Update(receipt);
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

        /// <summary>
        /// Delete a receipt
        /// </summary>
        /// <param name="id">Receipt id</param>
        public async Task DeleteAsync(int id)
        {
            var receipt = await _entityReceiptDao.GetByIDAsync(id);
            receipt.ReceiptImages.Clear();
            _entityReceiptDao.Delete(receipt);
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
                var receipt = await _entityReceiptDao.GetByIDAsync(receiptId);
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

        public async Task InsertImage(ReceiptImage image, int receiptId)
        {
            var receipt = await _entityReceiptDao.GetByIDAsync(receiptId);
            image.Receipts.Add(receipt);
            _entityReceiptImageDao.Insert(image);
        }

        public async Task<ReceiptImage> GetImage(int imageId)
        {
            var image = await _entityReceiptImageDao.GetAsync(x => x.Id == imageId);

            var receiptImages = image as ReceiptImage[] ?? image.ToArray();
            if (receiptImages.Count() > 1)
                throw new Exception("Duplicated receiptImage id!!!");

            return receiptImages.Count() == 1 ? receiptImages.ToArray()[0] : null;
        }

        public async Task<string> GetImageAddrsByReceiptId(int receiptId)
        {
            var receipt = await _entityReceiptDao.GetByIDAsync(receiptId);
            var sb = new StringBuilder();
            foreach (var image in receipt.ReceiptImages)
            {
                sb.Append(image.Id.ToString(CultureInfo.InvariantCulture) + ',' + image.Description + ';');
            }
            return sb.ToString();
        }

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
    }
}
