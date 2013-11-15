//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMate.DataObjects.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Receipts
    {
        public Receipts()
        {
            this.DepreciationAssets = new HashSet<DepreciationAssets>();
            this.Disposals = new HashSet<Disposals>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public System.DateTime CreatedBy { get; set; }
        public Nullable<decimal> Price { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public string Vendor { get; set; }
        public Nullable<decimal> GstRate { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<bool> IsBulk { get; set; }
        public string Note { get; set; }
        public Nullable<int> ReceiptType_Id { get; set; }
        public Nullable<int> ReceiptStatus_Id { get; set; }
        public Nullable<int> Currency_Id { get; set; }
        public string User_Id { get; set; }
        public byte[] Version { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Currencies Currencies { get; set; }
        public virtual ICollection<DepreciationAssets> DepreciationAssets { get; set; }
        public virtual ICollection<Disposals> Disposals { get; set; }
        public virtual ReceiptStatus ReceiptStatus { get; set; }
        public virtual ReceiptTypes ReceiptTypes { get; set; }
    }
}