using BusinessObjects;
using DataObjects;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataObjects.EntityFramework;
using WebUI.Models;

namespace WebUI.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly IReceiptDao _receiptDao = DataAccess.ReceiptDao;

        public IQueryable<ReceiptBrief> GetReceiptBReceipts(string userId)
        {
            return _receiptDao.GetReceiptBriefsByUser(userId);
        }

        public async Task<IQueryable<ReceiptBrief>> GetReceiptBriefsByUserAsync(string userId)
        {
            return await _receiptDao.GetReceiptBriefsByUserAsync(userId);
        }

        public Receipt GetReceipt(int receiptId)
        {
            return DataAccess.ReceiptDao.GetReceipt(receiptId);
        }

        public async Task<Receipt> GetReceiptAsync(int receiptId)
        {
            return await DataAccess.ReceiptDao.GetReceiptAsync(receiptId);
        }

        public async void SaveReceipt(Receipt receipt)
        {
            AspNetUsers user = await DataAccess.AspNetUserDao.GetByIdAsync(receipt.Id);
            DataAccess.ReceiptDao.SaveReceipt(receipt);

            var receiptImages = new ReceiptImages();
            receiptImages.AspNetUsers.Add(user);
            await DataAccess.ReceiptImageDao.SaveAsync(receiptImages);

        }

        public async Task SaveReceiptAsync(Receipt receipt)
        {
            await DataAccess.ReceiptDao.SaveReceiptAsync(receipt);
        }

        public void DeleteReceipt(ReceiptBrief receipt)
        {
            DataAccess.ReceiptDao.DeleteReceipt(receipt);
        }

        public async Task DeleteReceiptAsync(ReceiptBrief receipt)
        {
            await DataAccess.ReceiptDao.DeleteReceiptAsync(receipt);
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
    }
}