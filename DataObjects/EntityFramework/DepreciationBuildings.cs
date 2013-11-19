//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataObjects.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepreciationBuildings
    {
        public int Id { get; set; }
        public System.DateTime DatePurchased { get; set; }
        public string ConstructionMaterialsAndBuildingDescription { get; set; }
        public decimal Value { get; set; }
        public decimal RatePercentage { get; set; }
        public decimal DepreciationClaimed { get; set; }
        public decimal CloseingAdjustedTaxValue { get; set; }
        public decimal NumberOfMonth { get; set; }
        public string Note { get; set; }
        public int DepreciationMethond_Id { get; set; }
        public int RentalIncomeDetails_Id { get; set; }
    
        public virtual DepreciationMethonds DepreciationMethonds { get; set; }
        public virtual RentalIncomeDetails RentalIncomeDetails { get; set; }
    }
}
