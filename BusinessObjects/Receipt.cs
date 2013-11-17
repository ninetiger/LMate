using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BusinessObjects
{
    public class Receipt : BusinessObject
    {
        public Receipt()
        {
            Version = _versionDefault;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Description { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime CreatedBy { get; set; }

        [Required]
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

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Version { get; set; }
    }
}
