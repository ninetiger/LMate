using BusinessObjects;
using DataObjects.Shared;

namespace DataObjects.EntityFramework.ModelMapper
{
    /// <summary>
    /// Maps Entity Framework entities to business objects and vice versa.
    /// </summary>
    public class Mapper
    {
        #region Receipt
        /// <summary>
        /// Maps receipt entity to receipt business object.
        /// </summary>
        /// <param name="entity">A receipt entity to be transformed.</param>
        /// <returns>A receipt business object.</returns>
        internal static ReceiptViewModel Map(Receipt entity)
        {
            var viewModel = new ReceiptViewModel
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
                CurrencyId = entity.Currency_Id ?? 0,
                AccountTypeId = entity.AccountType_Id,
                UserId = entity.User_Id,
                Version = entity.Version.AsBase64String(),

                AccountTypeName  = entity.AccountType !=null ? entity.AccountType.Name : string.Empty,
                DepreciationAssets = entity.DepreciationAssets,
                Disposals = entity.Disposals,
                ReceiptCategoryType = entity.ReceiptCategory != null ? entity.ReceiptCategory.Type : string.Empty,
                VendorName = entity.Vendor !=null ? entity.Vendor.Name :　string.Empty,
                ReceiptImages = entity.ReceiptImages
            };

            return viewModel;
        }

        /// <summary>
        /// Maps receipt business object to receipt entity.
        /// </summary>
        /// <param name="viewModel">A receipt business object.</param>
        /// <returns>A receipt entity.</returns>
        internal static Receipt Map(ReceiptViewModel viewModel)
        {
            var receipt = new Receipt
            {
                Id = viewModel.Id,
                Description = viewModel.Description,
                Reference = viewModel.Reference,
                IsBulk = viewModel.IsBulk,
                PurchaseDate = viewModel.PurchaseDate,
                Price = viewModel.Price,
                IsIncludeTax = viewModel.IsIncludeTax,
                IsTaxExclusive = viewModel.IsTaxExclusive,
                GstRate = viewModel.GstRate,
                Tax = viewModel.Tax,
                Note = viewModel.Note,
                Vendor_Id = viewModel.VendorId,
                ReceiptCategory_Id = viewModel.ReceiptCategoryId,
                ReceiptStatus_Id = viewModel.ReceiptStatusId,
                Currency_Id = viewModel.CurrencyId,
                AccountType_Id = viewModel.AccountTypeId,
                User_Id = viewModel.UserId,
            };

            
            return receipt;
        }
        #endregion

        //#region  AccountType
        //internal static AccountType Map(AccountTypes entity)
        //{
        //    return new AccountType
        //    {
        //        Id = entity.Id,
        //        Type = entity.Type,
        //        Code = entity.Code,
        //        Name = entity.Name,
        //        Description = entity.Description,
        //        TaxRate = entity.TaxRate,
        //        UserId = entity.User_Id
        //    };
        //}
        //internal static AccountTypes Map(AccountType accountType)
        //{
        //    return new AccountTypes
        //    {
        //        Id = accountType.Id,
        //        Type = accountType.Type,
        //        Code = accountType.Code,
        //        Name = accountType.Name,
        //        Description = accountType.Description,
        //        TaxRate = accountType.TaxRate,
        //        User_Id = accountType.UserId,
        //    };
        //}
        //#endregion

        //#region Currency
        //public static Currency Map(Currencies entity)
        //{
        //    return new Currency
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name,
        //    };
        //}
        //public static Currencies Map(Currency bo)
        //{
        //    return new Currencies
        //    {
        //        Id = bo.Id,
        //        Name = bo.Name,
        //    };
        //}
        //#endregion

        //#region ReceiptCategory
        //public static ReceiptCategory Map(ReceiptCategories entity)
        //{
        //    return new ReceiptCategory
        //    {
        //        Id = entity.Id,
        //        Type = entity.Type,
        //        UserId = entity.User_Id,
        //        Version = entity.Version.AsBase64String()
        //    };
        //}

        //public static ReceiptCategories Map(ReceiptCategory bo)
        //{
        //    return new ReceiptCategories
        //    {
        //        Id = bo.Id,
        //        Type = bo.Type,
        //        User_Id = bo.UserId,
        //    };
        //}
        //#endregion

        //#region ReceiptImage
        //public static ReceiptImage Map(ReceiptImages entity)
        //{
        //    return new ReceiptImage
        //    {
        //        Id = entity.Id,
        //        ImageData = entity.ImageData,
        //        ImageMimeType = entity.ImageMimeType,
        //        Description = entity.Description,
        //        Date = entity.Date,
        //        IsActive = entity.IsActive,

        //        UserId = entity.AspNetUsers.Add(new AspNetUsers() { })
        //    };
        //}

        //public static ReceiptImages Map(ReceiptImage bo)
        //{
        //    return new ReceiptImages
        //    {
        //        Id = bo.Id,
        //        ImageData = bo.ImageData,
        //        ImageMimeType = bo.ImageMimeType,
        //        Description = bo.Description,
        //        Date = bo.Date,
        //        IsActive = bo.IsActive
        //    };
        //}
        //#endregion

    }
}
