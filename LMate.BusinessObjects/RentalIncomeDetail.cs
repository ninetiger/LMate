using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public class RentalIncomeDetail
    {
        public RentalIncomeDetail()
        {
            TaxUser = new TaxUser();
            TaxUser.Name = "wahaga:=";
            TaxUser.IRDNumber = 32424;

            RepairsAndMaintenance = new List<Receipt>();
            RepairsAndMaintenance.Add(new Receipt()
            {
                Description = "receipt 222",
                Price = (decimal)19.99,
                Type = ReceiptType.ReparisAndMaintenance
            });
            RepairsAndMaintenance.Add(new Receipt()
            {
                Description = "receipt 333",
                Price = 156.99m
            });

            OtherList = new List<Receipt>();
            OtherList.Add(new Receipt()
            {
                Description = "other1",
                Price = 15m,
                Type = ReceiptType.Other
            });
            OtherList.Add(new Receipt()
            {
                Description = "other 2",
                Price = (decimal)12.86,
                Type = ReceiptType.Insurance
            });
            OtherList.Add(new Receipt()
            {
                Description = "other 3",
                Price = (decimal)12.86,
                Type = ReceiptType.Other

            });
            BuildingDepreciation = new BuildingDepreciation();
            BuildingDepreciation.DatePurchased = DateTime.Now;
            BuildingDepreciation.DepreciationClaimed = 20.56m;
            BuildingDepreciation.DepreciationMethond = DepreciationMethod.DiminishingValueMethod;

            AssetDepreciations = new List<AssetDepreciation>();
            AssetDepreciations.Add(new AssetDepreciation()
            {
                DepreciationClaimed = 2.99m
            });
            AssetDepreciations.Add(new AssetDepreciation()
            {
                DepreciationClaimed = 7.99m
            });
        }

        //private void cal()
        //{
        //    //TotalIncome = TotalRents + OtherIncome + GainOrLossOnDisposal;

        //    decimal sumRepairsAndMaintenance = 0;
        //    if (RepairsAndMaintenance != null)
        //    {
        //        sumRepairsAndMaintenance =
        //            RepairsAndMaintenance.Where(x => x.Type == ReceiptType.ReparisAndMaintenance)
        //                .Sum(x => x.Price);
        //    }

        //    decimal sumOther = 0;
        //    if (OtherList != null)
        //    {
        //        sumOther = OtherList.Where(x => x.Type == ReceiptType.Other)
        //            .Sum(x => x.Price);
        //    }

        //    decimal sumBuilding =
        //        BuildingDepreciation != null ? BuildingDepreciation.CloseingAdjustedTaxValue : 0;

        //    decimal sumAsset = 0;
        //    if (AssetDepreciations != null)
        //    {
        //        sumAsset = AssetDepreciations.Select(x => x.DepreciationClaimed).Sum();
        //    }

        //    //TotalExpenses = Rates + Insurance + Interest + AgentCollectionFees
        //    //                    + sumRepairsAndMaintenance + sumOther + sumBuilding + sumAsset;
        //    NetRent = TotalIncome - TotalExpenses;

        //}

        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        private int _yearEnded = DateTime.Now.Year;
        [DisplayName("Year ended 31 March")]
        public int YearEnded
        {
            get
            {
                return _yearEnded;
            }
            set
            {
                if (_yearEnded < 2000) _yearEnded = 2000;
                else if (_yearEnded > 3000) _yearEnded = 3000;
                _yearEnded = value;
            }
        }

        public TaxUser TaxUser { get; set; }

        [DisplayName("Address of property rented")]
        public string AddressOfPropertyRented { get; set; }

        [DisplayName("Period the property was available for renting")]
        public int PeriodThePropertyWasAvailableForRenting { get; set; }

        //Income
        [DisplayName("Total rents")]
        [DataType(DataType.Currency)]
        public decimal TotalRents { get; set; }

        [DisplayName("Other income (specify)")]
        public decimal OtherIncome { get; set; }
        public string OtherIncomeSpecify { get; set; }

        [DisplayName("Gain or loss on disposal (enter any loss in brackets)")]
        public decimal GainOrLossOnDisposal { get; set; }

        [DisplayName("Total income")]
        public decimal TotalIncome
        {
            get
            {
                return TotalRents + OtherIncome + GainOrLossOnDisposal;
            }
        }

        //Expenses
        public decimal Rates { get; set; }
        public decimal Insurance { get; set; }
        public decimal Interest { get; set; }
        [DisplayName("Agent's collection fees")]
        public decimal AgentCollectionFees { get; set; }
        [DisplayName("Repairs and maintenance")]
        public List<Receipt> RepairsAndMaintenance { get; set; }
        public decimal SumRepairsAndMaintenance
        {
            get
            {
                return RepairsAndMaintenance != null ?
                       RepairsAndMaintenance
                           .Where(x => x.Type == ReceiptType.ReparisAndMaintenance)
                           .Sum(x => x.Price)
                       : 0;
            }
        }

        [DisplayName("Other (specify)")]
        public List<Receipt> OtherList { get; set; }
        public decimal SumOtherList
        {
            get
            {
                return OtherList != null
                    ? OtherList
                        .Where(x => x.Type == ReceiptType.Other)
                        .Sum(x => x.Price)
                    : 0;
            }
        }

        [DisplayName("Depreciation of buildings")]
        public BuildingDepreciation BuildingDepreciation { get; set; }

        [DisplayName("Depreciation of assets")]
        public List<AssetDepreciation> AssetDepreciations { get; set; }
        public decimal AssetDepreciationsSum
        {
            get
            {
                return AssetDepreciations != null ?
                    AssetDepreciations.Sum(x => x.DepreciationClaimed)
                    : 0;
            }
        }

        public decimal DepreciationSum
        {
            get
            {
                if (BuildingDepreciation == null)
                {
                    return AssetDepreciationsSum;
                }
                
                return BuildingDepreciation.DepreciationClaimed + AssetDepreciationsSum;
            }
        }


        [DisplayName("Total expenses")]
        public decimal TotalExpenses
        {
            get
            {
                return Rates + Insurance + Interest + AgentCollectionFees
                       + SumRepairsAndMaintenance + SumOtherList + DepreciationSum;
            }
        }

        [DisplayName("Net rents")]
        public decimal NetRent
        {
            get
            {
                return TotalIncome + TotalExpenses;
            }
        }

        public string Note { get; set; }
    }
}
