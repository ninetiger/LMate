using System.Collections.Generic;
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

        public ReceiptRepository()
        {
            _context = new LMateEntities();
            _entityAccountTypeDao = new EntityAccountTypeDao(_context);
            _entityReceiptDao = new EntityReceiptDao(_context);
            _entityCurrencyDao = new EntityCurrencyDao(_context);
            _entityReceiptImageDao = new EntityReceiptImageDao(_context);
        }

        public async Task<IQueryable<Receipt>> GetAllByUserIdAsync(string userId)
        {
            var list = await _entityReceiptDao.GetAsync(x => x.User_Id == userId);
            return list;
        }

        public async Task<IQueryable<ReceiptBriefViewModel>> GetReceiptBriefsByUserIdAsync(string userId)
        {
            var list = await _entityReceiptDao.GetAsync(x => x.User_Id == userId);
            return list.Select(x => new ReceiptBriefViewModel
            {
                Id = x.Id,
                Description = x.Description,
                PurchaseDate = x.PurchaseDate,
                Price = x.Price,
                Vendor = x.Vendor != null ? x.Vendor.Name : null,
                AccountType = x.AccountType != null ? x.AccountType.Type : null,
                IsBulk = x.IsBulk,
                HasImage = "No"
            });
        }

        public void Insert(Receipt entityToInsert)
        {
            _entityReceiptDao.Insert(entityToInsert);
        }

        public void Update(Receipt entityToUpdate)
        {
            _entityReceiptDao.Update(entityToUpdate);
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

        public async Task DeleteAsync(int id)
        {
            await _entityReceiptDao.DeleteAsync(id);
        }

        public async Task<ReceiptEditViewModel> GetReceiptForEditAsync(int receiptId, string userId)
        {
            Receipt receipt;
            if (receiptId == 0)
            {
                receipt = new Receipt {User_Id = userId};
            }
            else
            {
                receipt = await _entityReceiptDao.GetByIDAsync(receiptId);
            }

            return await GetReceiptForEditViewModelAsync(receipt);
        }

        public async Task<ReceiptEditViewModel> GetReceiptForEditViewModelAsync(Receipt receipt)
        {
            var accountTypeQuery = await _entityAccountTypeDao.GetAsync(x => x.User_Id == receipt.User_Id || x.User_Id == null);
            var accountTypeSelectList = accountTypeQuery.Select(x => new SelectListItem()
            {
                Selected = x.Id == (receipt.AccountType_Id ?? -1),
                Text = x.Type,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();

            var currencyQuery = await _entityCurrencyDao.GetAsync();
            var currenciesSelectList = currencyQuery.Select(x => new SelectListItem()
            {
                Selected = x.Id == receipt.Currency_Id,
                Text = x.Name,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();

            return new ReceiptEditViewModel
            {
                Receipt = receipt,
                AccountTypeSelectList = accountTypeSelectList,
                CurrencySelectList = currenciesSelectList,
            };
        }

        public void InsertImage(ReceiptImage image)
        {
            _entityReceiptImageDao.Insert(image);
        }

        public async Task<ReceiptImage[]>  GetImages(int receiptId)
        {
            var receipt = await _entityReceiptDao.GetAsync(x => x.Id == receiptId);
            if (receipt.Count() == 1)
            {
                return receipt.ToArray()[0].ReceiptImages.ToArray();
            }

            throw new Exception("Duplicated receipt id!!!");
        }

        public async Task<ReceiptImage> GetImage(int imageId)
        {
            var image = await _entityReceiptImageDao.GetAsync(x => x.Id == imageId);

            if (image.Count() > 1)
                throw new Exception("Duplicated receiptImage id!!!");

            return image.Count() == 1 ? image.ToArray()[0] : null;
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
