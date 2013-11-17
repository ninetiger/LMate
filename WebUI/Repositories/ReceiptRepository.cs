using BusinessObjects;
using DataObjects;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        public void SaveReceipt(Receipt receipt)
        {
            DataAccess.ReceiptDao.SaveReceipt(receipt);
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

            var receiptTypeQuery = DataAccess.ReceiptTypeDao.GetReceiptTypesByUser(userId);
            var receiptTypeSelectList = receiptTypeQuery.Select(x => new SelectListItem()
            {
                Selected = false,
                Text = x.Type,
                Value = x.Id.ToString()
            }).ToList();

            return new ReceiptEditViewModel
            {
                Receipt = receipt,
                ReceiptTypeSelectList = receiptTypeSelectList,
                CurrencySelectList = null
            };
        }
        public async Task<ReceiptEditViewModel> GetReceiptEditPostAsync(string userId, Receipt receipt)
        {

            var receiptTypeQuery = await DataAccess.ReceiptTypeDao.GetReceiptTypesByUserAsync(userId);
            var receiptTypeSelectList = receiptTypeQuery.Select(x => new SelectListItem()
            {
                Selected = false,
                Text = x.Type,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();

            return new ReceiptEditViewModel
            {
                Receipt = receipt,
                ReceiptTypeSelectList = receiptTypeSelectList,
                CurrencySelectList = null
            };
        }
    }
}