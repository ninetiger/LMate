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
    public class EFRentalIncomeRepository : IRentalIncomeRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<RentalIncome> RentalIncomes
        {
            get { return _context.RentalIncomes; }
        }


        public void SaveRentalIncome(RentalIncome rentalIncome)
        {
            if (rentalIncome.ID == 0)
            {
                _context.RentalIncomes.Add(rentalIncome);
            }
            else
            {
                RentalIncome dbEntry = _context.RentalIncomes.Find(rentalIncome.ID);
                if (dbEntry != null)
                {
                    dbEntry.YearEnded = rentalIncome.YearEnded;
                    dbEntry.TotalIncome = rentalIncome.TotalIncome;
                    dbEntry.TotalExpenses = rentalIncome.TotalExpenses;
                    dbEntry.NetRent = rentalIncome.NetRent;
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

        public RentalIncome DeleteRentalIncome(int id)
        {
            RentalIncome dbEntry = _context.RentalIncomes.Find(id);

            if (dbEntry != null)
            {
                _context.RentalIncomes.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
