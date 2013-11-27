using System;
using System.Collections.Generic;
using DataObjects.EntityFramework;

namespace BusinessObjects
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public bool IsBulk { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? Price { get; set; }
        public bool IsIncludeTax { get; set; }
        public bool IsTaxExclusive { get; set; }
        public decimal? GstRate { get; set; }
        public decimal? Tax { get; set; }
        public string Note { get; set; }
        public int? VendorId { get; set; }
        public int? ReceiptCategoryId { get; set; }
        public int? ReceiptStatusId { get; set; }
        public int? CurrencyId { get; set; }
        public int? AccountTypeId { get; set; }
        public string UserId { get; set; }
        public string Version { get; set; }

        public string AccountTypeName { get; set; }
        public ICollection<DepreciationAsset> DepreciationAssets { get; set; }
        public ICollection<Disposal> Disposals { get; set; }
        public string ReceiptCategoryType { get; set; }
        public ICollection<ReceiptChangeHistory> ReceiptChangeHistories { get; set; }
        public string VendorName { get; set; }
        public ICollection<ReceiptImage> ReceiptImages { get; set; }

//allow existing and new
    }
}
