using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BusinessObjects
{
    public class ReceiptViewModel
    {
        public ReceiptViewModel()
        {
            Version = "NotSet";
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        [DisplayName("Asset")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Description { get; set; }

        [StringLength(50, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string Reference { get; set; }

        [DisplayName("Multi Items Receipt")]
        public bool IsBulk { get; set; }

        [DisplayName("Date purchased")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PurchaseDate { get; set; }

        public decimal? Price { get; set; }

        [DisplayName("Include Tax")]
        public bool IsIncludeTax { get; set; }

        [DisplayName("Tax Exclusive")]
        public bool IsTaxExclusive { get; set; }
        
        [DisplayName("Gst Rate")]
        [Range(0, 100, ErrorMessage = "Range is 0 - 100")]
        public decimal? GstRate { get; set; }

        public decimal? Tax { get; set; }

        [StringLength(4000, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string Note { get; set; }

        [DisplayName("Vendor")]
        public int? VendorId { get; set; }

        [DisplayName("Receipt Category")]
        public int? ReceiptCategoryId { get; set; }

        [DisplayName("Receipt Status")]
        public int ReceiptStatusId { get; set; }

        [DisplayName("Currency")]
        public int? CurrencyId { get; set; }

        [DisplayName("Account Type")]
        public int? AccountTypeId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Version { get; set; }

        public string AccountTypeName { get; set; }
        public string VendorName { get; set; }

        public int[] ReceiptImageIds { get; set; }
    }
}
