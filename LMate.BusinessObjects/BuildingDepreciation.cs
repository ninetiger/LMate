using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public class BuildingDepreciation
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { set; get; }
        [HiddenInput(DisplayValue = false)]
        public int RentalIncomeID { set; get; }

        public DateTime DatePurchased { set; get; }
        public string ConstructionMaterialsAndBuildingDescription { set; get; }

        public DepreciationMethod DepreciationMethond { set; get; }
        public decimal Value { set; get; } //Straight line method (SL): Cost of buildings (excluding cost of land) Rate; Diminishing value method (DV): Opening Adjusted Tax Value
        public int RatePercentage { set; get; } //0-100%
        public decimal DepreciationClaimed { set; get; }
        public decimal CloseingAdjustedTaxValue { set; get; }

        //public decimal OriginalTaxValue { set; get; }
        
        public string Note { set; get; }
    }
}
