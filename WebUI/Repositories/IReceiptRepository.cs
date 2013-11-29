using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using WebUI.Models;

namespace WebUI.Repositories
{
    public interface IReceiptRepository : IRepository<ReceiptViewModel>
    {
        Task<IEnumerable<ReceiptBriefViewModel>> GetReceiptBriefsByUserIdAsync(string userId);

        Task<ReceiptEditViewModel> GetReceiptForEditAsync(int receiptId, string userId);

        Task<ReceiptEditViewModel> GetReceiptForEditViewModelAsync(ReceiptViewModel receipt);
    }
}
