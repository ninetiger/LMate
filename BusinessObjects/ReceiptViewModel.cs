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
        //[AllowHtml] //todo if allowed need to stop including <script> etc
        [StringLength(255, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string Description { get; set; }

        [StringLength(50, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string Reference { get; set; }

        [DisplayName("Multi Items Receipt")]
        public bool IsBulk { get; set; }

        [DisplayName("Date purchased")]
        public DateTime? PurchaseDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Range is 0 - 100")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal? Price
        {
            get
            {
                if (!_price.HasValue) return null;

                return decimal.Floor(_price.Value * 100) / 100;
            }
            set
            {
                _price = value;
            }
        }
        private decimal? _price;

        [DisplayName("Include Tax")]
        public bool IsIncludeTax { get; set; }

        [DisplayName("Tax Exclusive")]
        public bool IsTaxExclusive { get; set; }

        [DisplayName("Gst Rate")]
        [Range(0, 100, ErrorMessage = "Range is 0 - 100")]
        public decimal? GstRate
        {
            get
            {
                if (!_gstRate.HasValue) return null;
                return decimal.Floor(_gstRate.Value * 100) / 100;
            }
            set { _gstRate = value; }
        }
        private decimal? _gstRate;

        public decimal? Tax { get; set; }

        [StringLength(4000, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string Note { get; set; }

        [DisplayName("Vendor")]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be greater than 1")]
        public int? VendorId { get; set; }

        [DisplayName("Receipt Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be greater than 1")]
        public int? ReceiptCategoryId { get; set; }

        [DisplayName("Receipt Status")]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be greater than 1")]
        public int ReceiptStatusId { get; set; }

        [DisplayName("Currency")]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be greater than 1")]
        public int? CurrencyId { get; set; }

        [DisplayName("Account Type")]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be greater than 1")]
        public int? AccountTypeId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Version { get; set; }

        public string AccountTypeName { get; set; }
        public string VendorName { get; set; }
    }
}
