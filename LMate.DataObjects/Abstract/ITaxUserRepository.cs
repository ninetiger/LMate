using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMate.BusinessObjects;

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
