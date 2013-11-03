using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;

namespace LMate.DataObjects.Concrete
{
    public class EFRentalIncomeDetailRepository : IRentalIncomeDetailsRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<RentalIncomeDetail> RentalIncomeDetails
        {
            get { return _context.RentalIncomeDetails; }
        }

        public RentalIncomeDetail GetRentalIncomeDetail(int id)
        {
            return _context.RentalIncomeDetails.Find(id);
        }

        public IQueryable<RentalIncome> RentalIncomes
        {
            get
            {
                var queryable = RentalIncomeDetails.Select(r => new RentalIncome
                {
                    YearEnded = r.YearEnded,
                    TotalIncome = r.TotalRents + r.OtherIncome + r.GainOrLossOnDisposal,
                    TotalExpenses = r.Rates + r.Insurance + r.Interest + r.AgentCollectionFees
                       + r.SumRepairsAndMaintenance + r.SumOtherList + r.SumDepreciation,
                    NetRent = r.TotalIncome + r.TotalExpenses
                });

                //var rentalIncomeList = new List<RentalIncome>();
                //foreach (var item in rentalIncomeList)
                //{
                //    rentalIncomeList.Add(item);
                //}

                return queryable;
            }
        }

        public void SaveRentalIncomeDetail(RentalIncomeDetail rentalIncomeDetail)
        {
            if (rentalIncomeDetail.ID == 0)
            {
                _context.RentalIncomeDetails.Add(rentalIncomeDetail);
            }
            else
            {
                RentalIncomeDetail dbEntry = GetRentalIncomeDetail(rentalIncomeDetail.ID);
                if (dbEntry != null)
                {
                    dbEntry.YearEnded = rentalIncomeDetail.YearEnded;
                    //dbEntry.TotalIncome = rentalIncomeDetail.TotalIncome;
                    //dbEntry.TotalExpenses = rentalIncomeDetail.TotalExpenses;
                    //dbEntry.NetRent = rentalIncomeDetail.NetRent;
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public RentalIncomeDetail DeleteRentalIncomeDetail(int id)
        {
            RentalIncomeDetail dbEntry = _context.RentalIncomeDetails.Find(id);

            if (dbEntry != null)
            {
                _context.RentalIncomeDetails.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public RentalIncomeDetail GetNewRentalIncomeDetailBasedOnPrevYear()
        {
            var prevYearRentalIncomeDetail = RentalIncomeDetails.OrderByDescending(x => x.YearEnded).FirstOrDefault();
            
            //if (prevYearRentalIncomeDetail == null) return null;

            return prevYearRentalIncomeDetail;
        }
    }
}
