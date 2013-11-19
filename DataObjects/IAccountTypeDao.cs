using System.Linq;
using BusinessObjects;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface IAccountTypeDao
    {
        AccountType GetAccountType(int id);
        Task<AccountType> GetAccountTypeAsync(int id);

        IQueryable<AccountType> GetAccountTypesByUser(string userId, string sortExpression = "Id ASC");
        Task<IQueryable<AccountType>> GetAccountTypesByUserAsync(string userId, string sortExpression = "Id ASC");
 
    }
}
