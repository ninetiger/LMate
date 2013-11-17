using System;
using System.ComponentModel;
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


        [DisplayName("Asset")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Description { get; set; }

        [DisplayName("Date purchased")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PurchaseDate { get; set; }

        [DisplayName("Date added")] //todo make it read only and added autoly
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedBy { get; set; }

        [Required]
        public decimal? Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public string Vendor { get; set; }

        [DisplayName("Gst Rate")]
        public decimal? GstRate { get; set; }
        public decimal? Tax { get; set; }
        
        [DisplayName("Is Bulk")]
        public bool? IsBulk { get; set; }


        [StringLength(4000, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string Note { get; set; }

        [DisplayName("Receipt Type")]
        public int? ReceiptTypeId { get; set; }
        
        public int? ReceiptStatusId { get; set; }
        public int? CurrencyId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Version { get; set; }

    }
}
