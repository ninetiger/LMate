using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
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
                    Id = r.Id,
                    YearEnded = r.YearEnded,
                    TotalIncome = r.TotalIncome,
                    TotalExpenses = r.TotalExpenses
                });

                return queryable;
            }
        }

        public void SaveRentalIncomeDetail(RentalIncomeDetail rentalIncomeDetail)
        {
            if (rentalIncomeDetail.Id == 0)
            {
                _context.RentalIncomeDetails.Add(rentalIncomeDetail);
            }
            else
            {
                RentalIncomeDetail dbEntry = GetRentalIncomeDetail(rentalIncomeDetail.Id);
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
