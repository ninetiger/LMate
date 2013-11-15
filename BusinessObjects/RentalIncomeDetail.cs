using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using BusinessObjects;

namespace LMate.BusinessObjects
{
    public class RentalIncomeDetail
    {
        //public RentalIncomeDetail()
        //{
        //    TaxUser = new TaxUser();
        //    TaxUser.Name = "wahaga:=";
        //    TaxUser.IRDNumber = 32424;
            
        //    RepairsAndMaintenance = new List<Receipt>();
        //    RepairsAndMaintenance.Add(new Receipt()
        //    {
        //        Description = "receipt 222",
        //        Cost = (decimal)19.99,
        //        Type = ReceiptType.ReparisAndMaintenance,
        //        PurchaseDate = DateTime.Now
        //    });
        //    RepairsAndMaintenance.Add(new Receipt()
        //    {
        //        Description = "receipt 333",
        //        Cost = 156.99m,
        //        PurchaseDate = DateTime.Now
        //    });

        //    OtherList = new List<Receipt>();
        //    OtherList.Add(new Receipt()
        //    {
        //        Description = "other1",
        //        Cost = 15m,
        //        Type = ReceiptType.Other,
        //        PurchaseDate = DateTime.Now
        //    });
        //    OtherList.Add(new Receipt()
        //    {
        //        Description = "other 2",
        //        Cost = (decimal)12.86,
        //        Type = ReceiptType.Insurance,
        //        PurchaseDate = DateTime.Now
        //    });
        //    OtherList.Add(new Receipt()
        //    {
        //        Description = "other 3",
        //        Cost = (decimal)12.86,
        //        Type = ReceiptType.Other,
        //        PurchaseDate = DateTime.Now
        //    });
        //    DepreciationBuilding = new DepreciationBuilding();
        //    DepreciationBuilding.PurchaseDate = DateTime.Now;
        //    DepreciationBuilding.DepreciationClaimed = 20.56m;
        //    DepreciationBuilding.DepreciationMethond = DepreciationMethod.DiminishingValueMethod;

        //    DepreciationAssets = new List<DepreciationAsset>();
        //    //DepreciationAssets.Add(new DepreciationAsset()
        //    //{
        //    //    DepreciationClaimed = 2.99m
        //    //});
        //    DepreciationAssets.Add(new DepreciationAsset()
        //    {
        //        Receipt = new Receipt
        //        {
        //            Description="wahaha",
        //            PurchaseDate = DateTime.Now,
        //            Cost = 800.90m,
        //        },
        //        OpeningAdjustedTaxValue = 600.86m,
        //        RatePercentage = 20,
        //        DepreciationMethond = DepreciationMethod.DiminishingValueMethod,
        //        DepreciationClaimed = 7.99m,
        //        CloseingAdjustedTaxValue = 531.33m,
        //    });
        //}

        //private void cal()
        //{
        //    //TotalIncome = TotalRents + OtherIncome + GainOrLossOnDisposal;

        //    decimal sumRepairsAndMaintenance = 0;
        //    if (RepairsAndMaintenance != null)
        //    {
        //        sumRepairsAndMaintenance =
        //            RepairsAndMaintenance.Where(x => x.Type == ReceiptType.ReparisAndMaintenance)
        //                .Sum(x => x.Cost);
        //    }

        //    decimal sumOther = 0;
        //    if (OtherList != null)
        //    {
        //        sumOther = OtherList.Where(x => x.Type == ReceiptType.Other)
        //            .Sum(x => x.Cost);
        //    }

        //    decimal sumBuilding =
        //        DepreciationBuilding != null ? DepreciationBuilding.CloseingAdjustedTaxValue : 0;

        //    decimal sumAsset = 0;
        //    if (DepreciationAssets != null)
        //    {
        //        sumAsset = DepreciationAssets.Select(x => x.DepreciationClaimed).Sum();
        //    }

        //    //TotalExpenses = Rates + Insurance + Interest + AgentCollectionFees
        //    //                    + sumRepairsAndMaintenance + sumOther + sumBuilding + sumAsset;
        //    NetRent = TotalIncome - TotalExpenses;

        //}

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

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
        //todo need to updatedb to cal before uncomment this line
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
        public decimal TotalIncome
        {
            get
            {
                return TotalRents + OtherIncome + GainOrLossOnDisposal;
            }
            private set {/* needed for EF */ }
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
                //return RepairsAndMaintenance != null ?
                //       RepairsAndMaintenance
                //           .Where(x => x.Type == ReceiptType.ReparisAndMaintenance)
                //           .Sum(x => x.Cost)
                //       : 0;
                return 0;
            }
        }

        [DisplayName("Other (specify)")]
        public List<Receipt> OtherList { get; set; }
        public decimal SumOtherList
        {
            get
            {
                //return OtherList != null
                //    ? OtherList
                //        .Where(x => x.Type == ReceiptType.Other)
                //        .Sum(x => x.Cost)
                //    : 0;
                return 0;
            }
        }

        [DisplayName("Depreciation of buildings")]
        public DepreciationBuilding DepreciationBuilding { get; set; }

        [DisplayName("Depreciation of assets")]
        public List<DepreciationAsset> DepreciationAssets { get; set; }
        public decimal SumAssetDepreciations
        {
            get
            {
                return DepreciationAssets != null ?
                    DepreciationAssets.Sum(x => x.DepreciationClaimed)
                    : 0;
            }
        }

        public decimal SumDepreciation
        {
            get
            {
                if (DepreciationBuilding == null)
                {
                    return SumAssetDepreciations;
                }
                
                return DepreciationBuilding.DepreciationClaimed + SumAssetDepreciations;
            }
        }


        [DisplayName("Total expenses")]
        //todo need to updatedb to cal before uncomment this line
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
        public decimal TotalExpenses
        {
            get
            {
                return Rates + Insurance + Interest + AgentCollectionFees
                       + SumRepairsAndMaintenance + SumOtherList + SumDepreciation;
            }
            private set {/* needed for EF */ }
        }

        [DisplayName("Net rents")]
        //todo need to updatedb to cal before uncomment this line
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
        public decimal NetRent
        {
            get
            {
                return TotalIncome + TotalExpenses;
            }
            private set {/* needed for EF */ }
        }

        public string Note { get; set; }
    }
}
