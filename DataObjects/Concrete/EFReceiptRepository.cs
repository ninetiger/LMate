using BusinessObjects;
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
                    dbEntry.Id = receipt.Id;
                    dbEntry.Description = receipt.Description;
                    dbEntry.Reference = receipt.Reference;
                    dbEntry.IsBulk = receipt.IsBulk;
                    dbEntry.PurchaseDate = receipt.PurchaseDate;
                    dbEntry.Price = receipt.Price;
                    dbEntry.IsIncludeTax = receipt.IsIncludeTax;
                    dbEntry.IsTaxExclusive = receipt.IsTaxExclusive;
                    dbEntry.GstRate = receipt.GstRate;
                    dbEntry.Tax = receipt.Tax;
                    dbEntry.Note = receipt.Note;
                    dbEntry.VendorId = receipt.VendorId;
                    dbEntry.ReceiptCategoryId = receipt.ReceiptCategoryId;
                    dbEntry.ReceiptStatusId = receipt.ReceiptStatusId;
                    dbEntry.CurrencyId = receipt.CurrencyId;
                    dbEntry.AccountTypeId = receipt.AccountTypeId;
                    dbEntry.UserId = receipt.UserId;
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
