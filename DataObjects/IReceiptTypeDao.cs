using System.Linq;
using BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IReceiptTypeDao
    {
        ReceiptType GetReceiptType(int id);
        Task<ReceiptType> GetReceiptTypeAsync(int id);

        IQueryable<ReceiptType> GetReceiptTypesByUser(string userId, string sortExpression = "Id ASC");
        Task<IQueryable<ReceiptType>> GetReceiptTypesByUserAsync(string userId, string sortExpression = "Id ASC");
 
    }
}
