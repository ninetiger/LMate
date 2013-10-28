using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public enum ReceiptType
    {
        ReparisAndMaintenance, Asset, Other,
        Rate, Insurance, AgentCollectionFees
    }

        public enum Status
    {
        Depreciable, Sold, Disposed, Removed
    }

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

        public ReceiptType Type { set; get; }

        public Status Status { set; get; }
    }
}
