using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace LMate.DataObjects.Concrete
{
    public class EFTaxUserRepository : ITaxUserRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<TaxUser> TaxUsers
        {
            get { return _context.TaxUsers; }
        }

        public TaxUser GetTaxUser(int id)
        {
            return _context.TaxUsers.Find(id);
        }

        public void SaveTaxUser(TaxUser user)
        {
            if (user.ID == 0)
            {
                _context.TaxUsers.Add(user);
            }
            else
            {
                TaxUser dbEntry = GetTaxUser(user.ID);
                if (dbEntry != null)
                {
                    dbEntry.Name = user.Name;
                    dbEntry.IRDNumber = user.IRDNumber;
                    dbEntry.Address = user.Address;
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

        public TaxUser DeleteTaxUser(int id)
        {
            TaxUser dbEntry = _context.TaxUsers.Find(id);

            if (dbEntry != null)
            {
                _context.TaxUsers.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
