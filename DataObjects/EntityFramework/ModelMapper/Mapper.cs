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
                PurchaseDate = entity.PurchaseDate,
                CreatedBy = entity.CreatedBy,
                Price = entity.Price,
                ImageData = entity.ImageData,
                ImageMimeType = entity.ImageMimeType,
                Vendor = entity.Vendor,
                GstRate = entity.GstRate,
                Tax = entity.Tax,
                IsBulk = entity.IsBulk,
                Note = entity.Note,
                ReceiptTypeId = entity.ReceiptType_Id,
                ReceiptStatusId = entity.ReceiptStatus_Id,
                CurrencyId = entity.Currency_Id,
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
                PurchaseDate = receipt.PurchaseDate,
                CreatedBy = receipt.CreatedBy,
                Price = receipt.Price,
                ImageData = receipt.ImageData,
                ImageMimeType = receipt.ImageMimeType,
                Vendor = receipt.Vendor,
                GstRate = receipt.GstRate,
                Tax = receipt.Tax,
                IsBulk = receipt.IsBulk,
                Note = receipt.Note,
                ReceiptType_Id = receipt.ReceiptTypeId,
                ReceiptStatus_Id = receipt.ReceiptStatusId,
                Currency_Id = receipt.CurrencyId,
                User_Id = receipt.UserId,
            };
        }

        #region  ReceiptType
        internal static ReceiptType Map(ReceiptTypes entity)
        {
            return new ReceiptType
            {
                Id = entity.Id,
                Type = entity.Type,
                UserId = entity.User_Id
            };
        }
        internal static ReceiptTypes Map(ReceiptType receiptType)
        {
            return new ReceiptTypes
            {
                Id = receiptType.Id,
                Type = receiptType.Type,
                User_Id = receiptType.UserId,
            };
        }
        #endregion


    }
}
