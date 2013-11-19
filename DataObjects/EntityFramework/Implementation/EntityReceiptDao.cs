using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using BusinessObjects;
using DataObjects.EntityFramework.ModelMapper;
using LMate.DataObjects;

namespace DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IReceiptDao interface.
    /// </summary>
    public class EntityReceiptDao : IReceiptDao
    {
        /// <summary>
        /// Gets a receipt given a receipt identifier.
        /// </summary>
        /// <param name="receiptId">The receipt identifier.</param>
        /// <returns>The receipt.</returns>
        public Receipt GetReceipt(int receiptId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var query = context.Receipts.FirstOrDefault(c => c.Id == receiptId);
                return Mapper.Map(query);
            }
        }
        public async Task<Receipt> GetReceiptAsync(int receiptId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                //todo need to test receiptId == 0, -1, should return null
                var query = await context.Receipts.FirstOrDefaultAsync(c => c.Id == receiptId);
                return Mapper.Map(query);
            }
        }

        /// <summary>
        /// Gets list of receipts in given sortorder from every singal user!!!
        /// </summary>
        /// <param name="sortExpression">The required sort order.</param>
        /// <returns>List of receipts.</returns>
        public List<Receipt> GetAllReceipts(string sortExpression)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var list = new List<Receipt>();
                //todo remove .asqueryable() no need
                var receiptEntities = context.Receipts.AsQueryable().OrderBy(sortExpression).ToList(); //todo change to asyn list later
                foreach (var receipt in receiptEntities)
                    list.Add(Mapper.Map(receipt));

                return list;
            }
        }

        /// <summary>
        /// Gets list of receipts in given sortorder with a given userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sortExpression">The required sort order.</param>
        /// <returns>List of receipts.</returns>
        public IQueryable<Receipt> GetReceiptsByUser(string userId, string sortExpression)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entities = context.Receipts.AsQueryable().OrderBy(sortExpression).Where(c => c.User_Id == userId);
                //return entities.Select(receipt => Mapper.Map(receipt)).ToList();//todo change to asyn list later

                var list = new List<Receipt>();
                foreach (var receipt in entities)
                    list.Add(Mapper.Map(receipt));

                return list.AsQueryable();
            }
        }

        public IQueryable<ReceiptBrief> GetReceiptBriefsByUser(string userId, string sortExpression = "Id ASC")
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entities = context.Receipts
                    .OrderBy(sortExpression)
                    .Where(c => c.User_Id == userId)
                    .Select(entity => new ReceiptBrief
                    {
                        Id = entity.Id,
                        Description = entity.Description,
                        PurchaseDate = entity.PurchaseDate,
                        Price = entity.Price,
                        Vendor = entity.Vendors.Vendor,
                        IsBulk = entity.IsBulk,
                    });

                return entities.ToList().AsQueryable();
            }
        }

        public async Task<IQueryable<ReceiptBrief>> GetReceiptBriefsByUserAsync(string userId, string sortExpression = "Id ASC")
        {
           
                using (var context = DataObjectFactory.CreateContext())
                {
                    var entities = await context.Receipts
                        .OrderBy(sortExpression)
                        .Where(c => c.User_Id == userId)
                        .Select(entity => new ReceiptBrief
                        {
                            Id = entity.Id,
                            Description = entity.Description,
                            PurchaseDate = entity.PurchaseDate,
                            Price = entity.Price,
                            Vendor = entity.Vendors.Vendor,
                            IsBulk = entity.IsBulk,
                            HasImage = "nn"
                        }).ToListAsync();

                    return entities.AsQueryable();
                }
         
        }

        /// <summary>
        /// Gets the receipts between a given data range.
        /// </summary>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateThru">End date.</param>
        /// <returns></returns>
        public List<Receipt> GetReceiptsByDate(DateTime dateFrom, DateTime dateThru)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var receipts = context.Receipts.Where(o => o.PurchaseDate >= dateFrom && o.PurchaseDate <= dateThru).ToList();

                var list = new List<Receipt>();
                foreach (var receipt in receipts)
                    list.Add(Mapper.Map(receipt));

                return list;
            }
        }

        /// <summary>
        /// Insert new or update a receipt based on receipt id
        /// </summary>
        /// <param name="receipt">If the Id is 0, the recored is inserted as new.</param>
        public void SaveReceipt(Receipt receipt)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                Receipts entity;
                if (receipt.Id == 0)
                {
                    entity = Mapper.Map(receipt);
                    context.Receipts.Add(entity);
                }
                else
                {
                    entity = context.Receipts.Find(receipt.Id);
                    if (entity != null)
                    {
                        entity.Id = receipt.Id;
                        entity.Description = receipt.Description;
                        entity.Reference = receipt.Reference;
                        entity.IsBulk = receipt.IsBulk;
                        entity.PurchaseDate = receipt.PurchaseDate;
                        entity.Price = receipt.Price;
                        entity.IsIncludeTax = receipt.IsIncludeTax;
                        entity.IsTaxExclusive = receipt.IsTaxExclusive;
                        entity.GstRate = receipt.GstRate;
                        entity.Tax = receipt.Tax;
                        entity.Note = receipt.Note;
                        entity.Vendor_Id = receipt.VendorId;
                        entity.ReceiptCategory_Id = receipt.ReceiptCategoryId;
                        entity.ReceiptStatus_Id = receipt.ReceiptStatusId;
                        entity.Currency_Id = receipt.CurrencyId;
                        entity.AccountType_Id = receipt.AccountTypeId;
                        entity.User_Id = receipt.UserId;
                    }
                }

                try
                {
                    context.SaveChanges();
                    if (entity != null)
                    {
                        receipt.Version = Convert.ToBase64String(entity.Version); //todo need to test if return version is correct
                    }
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
        }
        public async Task SaveReceiptAsync(Receipt receipt)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                Receipts entity;
                if (receipt.Id == 0)
                {
                    entity = Mapper.Map(receipt);
                    context.Receipts.Add(entity);
                }
                else
                {
                    entity = await context.Receipts.FindAsync(receipt.Id);
                    if (entity != null)
                    {
                        entity.Id = receipt.Id;
                entity.Description = receipt.Description;
                entity.Reference = receipt.Reference;
                entity.IsBulk = receipt.IsBulk;
                entity.PurchaseDate = receipt.PurchaseDate;
                entity.Price = receipt.Price;
                 entity.IsIncludeTax=receipt.IsIncludeTax;
                entity.IsTaxExclusive = receipt.IsTaxExclusive;
                entity.GstRate = receipt.GstRate;
                entity.Tax = receipt.Tax;
                entity.Note = receipt.Note;
                entity.Vendor_Id = receipt.VendorId;
                entity.ReceiptCategory_Id = receipt.ReceiptCategoryId;
                entity.ReceiptStatus_Id = receipt.ReceiptStatusId;
                entity.Currency_Id = receipt.CurrencyId;
                entity.AccountType_Id = receipt.AccountTypeId;
                        entity.User_Id = receipt.UserId;
                    }
                }

                try
                {
                    await context.SaveChangesAsync();
                    if (entity != null)
                    {
                        receipt.Version = Convert.ToBase64String(entity.Version); //todo need to test if return version is correct
                    }
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
        }

        /// <summary>
        /// Deletes a receipt record from the database.
        /// </summary>
        /// <param name="receipt">The receipt to be deleted.</param>
        /// <returns>Number of rows affected.</returns>
        public void DeleteReceipt(ReceiptBrief receipt)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Receipts.SingleOrDefault(c => c.Id == receipt.Id);
                if (entity != null)
                {
                    context.Receipts.Remove(entity);
                    context.SaveChanges();
                }
            }
        }
        public async Task DeleteReceiptAsync(ReceiptBrief receipt)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = await context.Receipts.SingleOrDefaultAsync(c => c.Id == receipt.Id);
                if (entity != null)
                {
                    context.Receipts.Remove(entity);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
