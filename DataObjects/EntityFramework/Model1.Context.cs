﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LMateEntities : DbContext
    {
        public LMateEntities()
            : base("name=LMateEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<DepreciationAsset> DepreciationAssets { get; set; }
        public virtual DbSet<DepreciationBuilding> DepreciationBuildings { get; set; }
        public virtual DbSet<DepreciationMethod> DepreciationMethods { get; set; }
        public virtual DbSet<Disposal> Disposals { get; set; }
        public virtual DbSet<DisposalType> DisposalTypes { get; set; }
        public virtual DbSet<ReceiptCategory> ReceiptCategories { get; set; }
        public virtual DbSet<ReceiptChangeHistory> ReceiptChangeHistories { get; set; }
        public virtual DbSet<ReceiptImage> ReceiptImages { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<ReceiptStatus> ReceiptStatuses { get; set; }
        public virtual DbSet<RentalIncomeDetail> RentalIncomeDetails { get; set; }
        public virtual DbSet<TaxUser> TaxUsers { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
    }
}
