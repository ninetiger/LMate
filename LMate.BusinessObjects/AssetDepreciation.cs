using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public enum DepreciationMethod
    {
        StraightLineMethod, DiminishingValueMethod
    }

    public class AssetDepreciation
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ReceiptID { get; set; }

        public decimal OpeningAdjustedTaxValue { get; set; }
        public decimal CloseingAdjustedTaxValue { get; set; }
        public decimal DepreciationRate { get; set; }
        public DepreciationMethod DepreciationMethond { get; set; }
        public int NumberOfMonth { get; set; }
        public decimal DepreciationClaimed { get; set; }
        public string Note { get; set; }

    }
}
