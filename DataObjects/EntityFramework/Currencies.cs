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
    
    public partial class Currencies
    {
        public Currencies()
        {
            this.Receipts = new HashSet<Receipts>();
        }
    
        public int Id { get; set; }
        public string Currency { get; set; }
    
        public virtual ICollection<Receipts> Receipts { get; set; }
    }
}
