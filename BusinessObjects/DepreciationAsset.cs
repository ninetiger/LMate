using System.ComponentModel;

namespace LMate.BusinessObjects
{
    public class DepreciationAsset : Depreciation
    {
        public Receipt Receipt { get; set; }

        [DisplayName("Opening adjusted tax value")]
        public decimal OpeningAdjustedTaxValue { get; set; }
    }
}
