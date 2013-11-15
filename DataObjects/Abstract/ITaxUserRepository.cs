using LMate.BusinessObjects;
using System.Linq;

namespace LMate.DataObjects.Abstract
{
    public interface ITaxUserRepository
    {
        IQueryable<TaxUser> TaxUsers { get; }

        TaxUser GetTaxUser(int id);

        void SaveTaxUser(TaxUser user);
        
        TaxUser DeleteTaxUser(int id);
    }
}
