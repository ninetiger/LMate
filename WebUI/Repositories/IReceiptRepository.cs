using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects.EntityFramework;
using WebUI.Models;

namespace WebUI.Repositories
{
    public interface IReceiptRepository : IRepository<ReceiptViewModel>
    {
        Task<Receipt> GetReceiptSecure(int receiptId, string userId);

        Task<IEnumerable<ReceiptBriefViewModel>> GetReceiptBriefsByUserIdAsync(string userId);

        Task<ReceiptEditViewModel> GetReceiptForEditAsync(int receiptId, string userId);

        Task<ReceiptEditViewModel> GetReceiptForEditViewModelAsync(ReceiptViewModel receipt);

        #region ReceiptImage
        Task InsertImage(ReceiptImage image, int receiptId, string userId);

        Task<ReceiptImage> GetImageSecure(int imageId, string userId);

        Task<string> GetImageAddrsByReceiptId(int receiptId, string userId);

        Task DetachAnImageFromReceipt(int imageId, int receiptId, string userId);
        #endregion

        #region Vendor
        Task<Vendor> GetVednorSecure(int vendorId, string userId);
        Task<Vendor> GetVednorSecure(string vendorName, string userId);
        Task<string[]> SearchVendorNameSecure(string searchString, string userId);
        //void InsertVendor(Vendor vendor);
        #endregion

    }
}
