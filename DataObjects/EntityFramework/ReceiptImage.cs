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
    
    public partial class ReceiptImage
    {
        public ReceiptImage()
        {
            this.Receipts = new HashSet<Receipt>();
        }
    
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public string User_Id { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
