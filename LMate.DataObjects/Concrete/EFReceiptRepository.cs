using LMate.BusinessObjects;
using LMate.DataObjects.Abstract;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace LMate.DataObjects.Concrete
{
    public class EFReceiptRepository : IReceiptRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<Receipt> Receipts
        {
            get { return _context.Receipts; }
        }

        public void SaveReceipt(Receipt receipt)
        {
            if (receipt.Id == 0)
            {
                _context.Receipts.Add(receipt);
            }
            else
            {
                Receipt dbEntry = _context.Receipts.Find(receipt.Id);
                if (dbEntry != null)
                {
                    dbEntry.Description = receipt.Description;
                    dbEntry.DatePurchased = receipt.DatePurchased;
                    dbEntry.Cost = receipt.Cost;
                    dbEntry.ImageData = receipt.ImageData;
                    dbEntry.ImageMimeType = receipt.ImageMimeType;
                    dbEntry.Note = receipt.Note;
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

        public Receipt DeleteReceipt(int id)
        {
            Receipt dbEntry = _context.Receipts.Find(id);

            if (dbEntry != null)
            {
                _context.Receipts.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
