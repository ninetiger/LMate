using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public class RentalIncomeDetail : RentalIncome
    {
        public string Name { set; get; }
        public string IRDNumber { set; get; }

        public string AddressOfPropertyRented { set; get; }
        public int PeriodThePropertyWasAvailableForRenting { set; get; }

        //Income
        public decimal TotalRent { set; get; }
        public decimal OtherIncome { set; get; }
        public decimal GainOrLossOnDisposal { set; get; }

        //Expenses
        public decimal Rates { set; get; }
        public decimal Insurance { set; get; }
        public decimal Interest { set; get; }
        public decimal AgentCollectionFees { set; get; }

        public List<Receipt> RepairsAndMaintenance { set; get; } 
        public List<Receipt> Other { set; get; } 
        
        public List<AssetDepreciation> AssetDepreciations { set; get; }

        public string Note { set; get; }
    }
}
