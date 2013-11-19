using BusinessObjects;
using DataObjects.Shared;

namespace DataObjects.EntityFramework.ModelMapper
{
    /// <summary>
    /// Maps Entity Framework entities to business objects and vice versa.
    /// </summary>
    public class Mapper
    {
        /// <summary>
        /// Maps receipt entity to receipt business object.
        /// </summary>
        /// <param name="entity">A receipt entity to be transformed.</param>
        /// <returns>A receipt business object.</returns>
        internal static Receipt Map(Receipts entity)
        {
            return new Receipt
            {
                Id = entity.Id,
                Description = entity.Description,
                Reference = entity.Reference,
                IsBulk = entity.IsBulk,
                PurchaseDate = entity.PurchaseDate,
                Price = entity.Price,
                IsIncludeTax = entity.IsIncludeTax,
                IsTaxExclusive = entity.IsTaxExclusive,
                GstRate = entity.GstRate,
                Tax = entity.Tax,
                Note = entity.Note,
                VendorId = entity.Vendor_Id,
                ReceiptCategoryId = entity.ReceiptCategory_Id,
                ReceiptStatusId = entity.ReceiptStatus_Id,
                CurrencyId = entity.Currency_Id,
                AccountTypeId = entity.AccountType_Id,
                UserId = entity.User_Id,
                Version = entity.Version.AsBase64String(),

                
            };
        }

        /// <summary>
        /// Maps receipt business object to receipt entity.
        /// </summary>
        /// <param name="receipt">A receipt business object.</param>
        /// <returns>A receipt entity.</returns>
        internal static Receipts Map(Receipt receipt)
        {
            return new Receipts
            {
                Id = receipt.Id,
                Description = receipt.Description,
                Reference = receipt.Reference,
                IsBulk = receipt.IsBulk,
                PurchaseDate = receipt.PurchaseDate,
                Price = receipt.Price,
                 IsIncludeTax=receipt.IsIncludeTax,
                IsTaxExclusive = receipt.IsTaxExclusive,
                GstRate = receipt.GstRate,
                Tax = receipt.Tax,
                Note = receipt.Note,
                Vendor_Id = receipt.VendorId,
                ReceiptCategory_Id = receipt.ReceiptCategoryId,
                ReceiptStatus_Id = receipt.ReceiptStatusId,
                Currency_Id = receipt.CurrencyId,
                AccountType_Id = receipt.AccountTypeId,
                User_Id = receipt.UserId,
            };
        }

        #region  AccountType
        internal static AccountType Map(AccountTypes entity)
        {
            return new AccountType
            {
                Id = entity.Id,
                Type = entity.Type,
                Code = entity.Code,
                Name = entity.Name,
                Description = entity.Description,
                TaxRate = entity.TaxRate,
                UserId = entity.User_Id
            };
        }
        internal static AccountTypes Map(AccountType accountType)
        {
            return new AccountTypes
            {
                Id = accountType.Id,
                Type = accountType.Type,
                Code = accountType.Code,
                Name = accountType.Name,
                Description = accountType.Description,
                TaxRate = accountType.TaxRate,
                User_Id = accountType.UserId,
            };
        }
        #endregion


    }
}
