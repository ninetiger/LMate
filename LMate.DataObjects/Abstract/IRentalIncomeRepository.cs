using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMate.BusinessObjects;

namespace LMate.DataObjects.Abstract
{
    public interface IRentalIncomeRepository
    {
        IQueryable<RentalIncome> RentalIncomes { get; }
        
        void SaveRentalIncome(RentalIncome rentalIncome);
        
        RentalIncome DeleteRentalIncome(int id);
    }
}
