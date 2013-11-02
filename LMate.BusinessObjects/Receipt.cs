using System;
using System.ComponentModel;
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
        public int ReceiptID { get; set; }

        public string Description { get; set; }

        [DisplayName("Date purchased")]
        public DateTime DatePurchased { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public string Comment { get; set; }

        public ReceiptType Type { get; set; }

        public Status Status { get; set; }
    }
}
