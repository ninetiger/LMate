using System.Globalization;
using System.Linq;
using BusinessObjects;
using DataObjects.Abstract;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DataObjects.Concrete
{
    public class EFReceiptRepository : IReceiptRepository
    {
        private readonly LMateEntities _context;
        private readonly EntityReceiptDao _entityReceiptDao;

        public EFReceiptRepository()
        {
            _context = new LMateEntities();
            _entityReceiptDao = new EntityReceiptDao(_context);
        }

        public async Task<IEnumerable<Receipt>> GetAllByUserIdAsync(string userId)
        {
            var list = await _entityReceiptDao.GetAsync(filter: x => x.User_Id == userId);
            return list;
        }

        public async Task<IEnumerable<ReceiptBrief>> GetReceiptBriefsByUserAsync(string userId)
        {
            var list = await _entityReceiptDao.GetAsync(x => x.User_Id == userId);
            return list.Select(x => new ReceiptBrief()
            {
                Id = x.Id,
                Description = x.Description,
                PurchaseDate = x.PurchaseDate,
                Price = x.Price,
                Vendor = x.Vendor.Name,
                AccountType = x.AccountType.Type,
                IsBulk = x.IsBulk,
                HasImage = "No", //todo
            });
        }

        public async Task InsertAsync(Receipt entityToInsert)
        {
            _entityReceiptDao.Insert(entityToInsert);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Receipt entityToUpdate)
        {
            var entity = _entityReceiptDao.GetByIDAsync(entityToUpdate.Id);
            if (entity != null)
            {
                _entityReceiptDao.Update(entityToUpdate);
            }
            await SaveChangesAsync();
        }

        private async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _entityReceiptDao.Context.SaveChangesAsync();
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
            await SaveChangesAsync();
        }

        public async Task<ReceiptEditViewModel> GetReceiptEditAsync(int receiptId, string userId)
        {
            Receipt receipt;
            if (receiptId == 0)
                receipt = new Receipt();
            else
                receipt = await DataAccess.ReceiptDao.GetReceiptAsync(receiptId);

            var accountTypeQuery = DataAccess.AccountTypeDao.GetAccountTypesByUser(userId);
            var accountTypeSelectList = accountTypeQuery.Select(x => new SelectListItem()
            {
                Selected = x.Id == receipt.AccountTypeId,
                Text = x.Type,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();

            var currencyQuery = DataAccess.CurrencyDao.GetAll();
            var currenciesSelectList = currencyQuery.Select(x => new SelectListItem()
            {
                Selected = x.Id == receipt.CurrencyId,
                Text = x.Name,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();

            var imageList = DataAccess.ReceiptImageDao.GetAllByUserId(userId);

            return new ReceiptEditViewModel
                    {
                        Receipt = receipt,
                        AccountTypeSelectList = accountTypeSelectList,
                        CurrencySelectList = currenciesSelectList,
                        ImageList = imageList //todo immage
                    };
        }
        public async Task<ReceiptEditViewModel> GetReceiptEditPostAsync(string userId, Receipt receipt)
        {

            var accountTypeQuery = await DataAccess.AccountTypeDao.GetAccountTypesByUserAsync(userId);
            var accountTypeSelectList = accountTypeQuery.Select(x => new SelectListItem()
            {
                Selected = false,
                Text = x.Type,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();

            return new ReceiptEditViewModel
            {
                Receipt = receipt,
                AccountTypeSelectList = accountTypeSelectList,
                CurrencySelectList = null
            };
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
