using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public class Receipt
    {
        [HiddenInput(DisplayValue = false)]
        public int ReceiptID { set; get; }

        public string Description { set; get; }

        public DateTime DatePurchased { set; get; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public string Comment { set; get; }

       // public decimal OpeningAdjustedTaxValue { set; get; }
       // public decimal DepreciationRate { set; get; }
    }
}
