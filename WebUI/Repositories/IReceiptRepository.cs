using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects;
using WebUI.Models;

namespace WebUI.Repositories
{
    public interface IReceiptRepository
    {
        IQueryable<ReceiptBrief> GetReceiptBReceipts(string userId);

        Task<IQueryable<ReceiptBrief>> GetReceiptBriefsByUserAsync(string userId);

        Receipt GetReceipt(int receiptId);
        Task<Receipt> GetReceiptAsync(int receiptId);

        void SaveReceipt(Receipt receipt);
        Task SaveReceiptAsync(Receipt receipt);

        void DeleteReceipt(ReceiptBrief receipt);
        Task DeleteReceiptAsync(ReceiptBrief receipt);

        Task<ReceiptEditViewModel> GetReceiptEditAsync(int receiptId, string userId);
        Task<ReceiptEditViewModel> GetReceiptEditPostAsync(string userId, Receipt receipt);
    }

}