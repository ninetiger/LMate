using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public enum Method
    {
        DV, ST    
    }

    public class Return
    {
        [HiddenInput(DisplayValue = false)]
        public int ReturnID { set; get; }

        public decimal OpeningAdjustedTaxValue { set; get; }
        public decimal CloseingAdjustedTaxValue { set; get; }
        public decimal DepreciationRate { set; get; }
        public Method DepreciationMethond { set; get; }
        public int NumberOfMonth { set; get; }
        public decimal DepreciationClaimed { set; get; }
        public string Note { set; get; }

    }
}
