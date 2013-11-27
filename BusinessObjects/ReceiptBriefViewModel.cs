using System;

namespace BusinessObjects
{
    public class ReceiptBriefViewModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? Price { get; set; }
        public string Vendor { get; set; }
        public string AccountType { get; set; }
        public bool IsBulk { get; set; }
        public string HasImage { get; set; }
    }
}
