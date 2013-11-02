using System.ComponentModel;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public enum DepreciationMethod
    {
        StraightLineMethod, DiminishingValueMethod
    }

    public abstract class Depreciation
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [DisplayName("Depreciation methond")]
        public DepreciationMethod DepreciationMethond { get; set; }
        
        [DisplayName("Rate")]
        public int RatePercentage { get; set; } //0-100%

        [DisplayName("Depreciation claimed")]
        public decimal DepreciationClaimed { get; set; }

        [DisplayName("Closeing adjusted tax value")]
        public decimal CloseingAdjustedTaxValue { get; set; }


        public int NumberOfMonth { get; set; }
        public string Note { get; set; }
    }
}
