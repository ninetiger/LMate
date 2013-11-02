using System;
using System.ComponentModel;

namespace LMate.BusinessObjects
{
    public class DepreciationBuilding : Depreciation
    {

        [DisplayName("Date purchased")]
        public DateTime DatePurchased { get; set; }

        [DisplayName("Construction materials and building description")]
        public string ConstructionMaterialsAndBuildingDescription { get; set; }

    
        public decimal Value { get; set; } //Straight line method (SL): Cost of buildings (excluding cost of land) Rate; Diminishing value method (DV): Opening Adjusted Tax Value
        


        //public decimal OriginalTaxValue { get; set; }
        
    }
}
