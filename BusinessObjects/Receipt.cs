using System;
using LMate.BusinessObjects;

namespace BusinessObjects
{
    public class Receipt : BusinessObject
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime CreatedBy { get; set; }
        public decimal? Price { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public string Vendor { get; set; }
        public decimal? GstRate { get; set; }
        public decimal? Tax { get; set; }
        public bool? IsBulk { get; set; }
        public string Note { get; set; }
        public int? ReceiptTypeId { get; set; }
        public int? ReceiptStatusId { get; set; }
        public int? CurrencyId { get; set; }
        public string UserId { get; set; }
        public string Version { get; set; }
    }
}
