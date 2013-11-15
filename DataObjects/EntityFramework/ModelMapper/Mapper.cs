using BusinessObjects;
using LMate.DataObjects.Shared;

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
                Version = entity.Version.AsBase64String()
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

        ///// <summary>
        ///// Maps order entity to order business object.
        ///// </summary>
        ///// <param name="entity">An order entity.</param>
        ///// <returns>An order business object.</returns>
        //internal static Order Map(OrderEntity entity)
        //{
        //    return new Order
        //    {
        //        OrderId = entity.OrderId,
        //        Freight = entity.Freight.HasValue ? (float)entity.Freight : default(float),
        //        OrderDate = entity.OrderDate,
        //        RequiredDate = entity.RequiredDate.HasValue ? (DateTime)entity.RequiredDate : default(DateTime),
        //        Version = entity.Version.AsBase64String()
        //    };
        //}

        ///// <summary>
        ///// Maps order detail entity to order detail business object.
        ///// </summary>
        ///// <param name="entity">An order detail entity.</param>
        ///// <returns>An order detail business object.</returns>
        //internal static OrderDetail Map(OrderDetailEntity entity)
        //{
        //    return new OrderDetail
        //    {
        //        ProductName = entity.Product == null ? "" : entity.Product.ProductName,
        //        Discount = (float)entity.Discount,
        //        Quantity = entity.Quantity,
        //        UnitPrice = (float)entity.UnitPrice,
        //        Version = entity.Version.AsBase64String()
        //    };
        //}

        ///// <summary>
        ///// Maps product category entity to category business object.
        ///// </summary>
        ///// <param name="entity">A category entity.</param>
        ///// <returns>A category business object.</returns>
        //internal static Category Map(CategoryEntity entity)
        //{
        //    return new Category
        //    {
        //        CategoryId = entity.CategoryId,
        //        Description = entity.Description,
        //        Name = entity.CategoryName,
        //        Version = entity.Version.AsBase64String()
        //    };
        //}


        ///// <summary>
        ///// Maps product entity to product business object.
        ///// </summary>
        ///// <param name="entity">A product entity.</param>
        ///// <returns>A product business object.</returns>
        //internal static Product Map(ProductEntity entity)
        //{
        //    return new Product
        //    {
        //        ProductId = entity.ProductId,
        //        ProductName = entity.ProductName,
        //        UnitPrice = (double)entity.UnitPrice,
        //        UnitsInStock = entity.UnitsInStock,
        //        Weight = entity.Weight,
        //        Version = entity.Version.AsBase64String()
        //    };
        //}
    }
}
