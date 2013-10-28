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
        public int ID { set; get; }
        public int ReceiptID { set; get; }

        public decimal OpeningAdjustedTaxValue { set; get; }
        public decimal CloseingAdjustedTaxValue { set; get; }
        public decimal DepreciationRate { set; get; }
        public DepreciationMethod DepreciationMethond { set; get; }
        public int NumberOfMonth { set; get; }
        public decimal DepreciationClaimed { set; get; }
        public string Note { set; get; }

    }
}
