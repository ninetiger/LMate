using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMate.BusinessObjects;

namespace LMate.DataObjects.Abstract
{
    public interface IRentalIncomeDetailsRepository
    {
        IQueryable<RentalIncomeDetail> RentalIncomeDetails { get; }

        RentalIncomeDetail GetRentalIncomeDetail(int id);

        List<RentalIncome> GetRentalIncomes();
        
        void SaveRentalIncomeDetail(RentalIncomeDetail rentalIncomeDetail);

        RentalIncomeDetail DeleteRentalIncomeDetail(int id);

        RentalIncomeDetail GetNewRentalIncomeDetailBasedOnPrevYear();
    }
}
