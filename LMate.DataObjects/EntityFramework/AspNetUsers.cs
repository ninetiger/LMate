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
    
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaims>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogins>();
            this.Receipts = new HashSet<Receipts>();
            this.RentalIncomeDetails = new HashSet<RentalIncomeDetails>();
            this.TaxUsers = new HashSet<TaxUsers>();
            this.UserDelegates1 = new HashSet<UserDelegates>();
            this.AspNetRoles = new HashSet<AspNetRoles>();
        }
    
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string Discriminator { get; set; }
    
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<Receipts> Receipts { get; set; }
        public virtual ICollection<RentalIncomeDetails> RentalIncomeDetails { get; set; }
        public virtual ICollection<TaxUsers> TaxUsers { get; set; }
        public virtual UserDelegates UserDelegates { get; set; }
        public virtual ICollection<UserDelegates> UserDelegates1 { get; set; }
        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
    }
}
