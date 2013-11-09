using LMate.BusinessObjects;
using System.Linq;

namespace LMate.DataObjects.Abstract
{
    public interface IReceiptRepository
    {
        IQueryable<Receipt> Receipts { get; }

        void SaveReceipt(Receipt receipt);

        Receipt DeleteReceipt(int id);
    }
}
