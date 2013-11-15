using BusinessObjects;
using LMate.BusinessObjects;
using System.Linq;

namespace LMate.DataObjects.Abstract
{
    public interface IRentalIncomeDetailsRepository
    {
        IQueryable<RentalIncomeDetail> RentalIncomeDetails { get; }

        RentalIncomeDetail GetRentalIncomeDetail(int id);

        IQueryable<RentalIncome> RentalIncomes { get; }
        
        void SaveRentalIncomeDetail(RentalIncomeDetail rentalIncomeDetail);

        RentalIncomeDetail DeleteRentalIncomeDetail(int id);

        RentalIncomeDetail GetNewRentalIncomeDetailBasedOnPrevYear();
    }
}
