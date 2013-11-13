using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public enum ReceiptType
    {
        Improvement, ReparisAndMaintenance, Asset, Other,
        Rate, Insurance, AgentCollectionFees,
        MotorVehicleExpenses, LegalExpenses, Interest,
        ComputerEquipment, TelephoneInternet, OfficeEquipment,
        OfficeExpenses, PrintingStationery, Rent
    }

    public enum Status
    {
        Depreciable, Sold, Disposed, Removed
    }

    public class Receipt
    {
        public Receipt()
        {
            DatePurchased = DateTime.Now;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        //[ForeignKey("AspNetUsers")]
        //public string UserId { get; set; }

        [DisplayName("Asset")]
        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Description { get; set; }

        [DisplayName("Date purchased")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatePurchased { get; set; }

        [Range(0.01, 79228162514264337593543950335.0, ErrorMessage = "Please enter a positive price")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [StringLength(4000, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string Note { get; set; }


        public ReceiptType Type { get; set; }

        public Status Status { get; set; }

        public string Vendor { get; set; }
        public string Currency { get; set; }
        public decimal GstRate { get; set; }
        public decimal Tax { get; set; }
        public bool IsBulk { get; set; }
        public string Category { get; set; } //motor, rent,repairs, printing

        public int RentalIncomeDetailId { get; set; }
        public virtual RentalIncomeDetail RentalIncomeDetail { get; set; }

    }
}
