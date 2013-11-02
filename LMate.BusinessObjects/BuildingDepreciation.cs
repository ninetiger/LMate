using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public class BuildingDepreciation
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [DisplayName("Date purchased")]
        public DateTime DatePurchased { get; set; }

        [DisplayName("Construction materials and building description")]
        public string ConstructionMaterialsAndBuildingDescription { get; set; }

        [DisplayName("Depreciation methond")]
        public DepreciationMethod DepreciationMethond { get; set; }
        
        public decimal Value { get; set; } //Straight line method (SL): Cost of buildings (excluding cost of land) Rate; Diminishing value method (DV): Opening Adjusted Tax Value
        
        [DisplayName("Rate")]
        public int RatePercentage { get; set; } //0-100%

        [DisplayName("Depreciation claimed")]
        public decimal DepreciationClaimed { get; set; }

        [DisplayName("Closeing adjusted tax value")]
        public decimal CloseingAdjustedTaxValue { get; set; }

        //public decimal OriginalTaxValue { get; set; }
        
        public string Note { get; set; }
    }
}
