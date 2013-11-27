using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects.EntityFramework;
using WebUI.Models;

namespace WebUI.Repositories
{
    public interface IReceiptRepository : IRepository<Receipt>
    {
        Task<IQueryable<ReceiptBriefViewModel>> GetReceiptBriefsByUserIdAsync(string userId);

        Task<ReceiptEditViewModel> GetReceiptForEditAsync(int receiptId, string userId);

        Task<ReceiptEditViewModel> GetReceiptForEditViewModelAsync(Receipt receipt);
    }
}
