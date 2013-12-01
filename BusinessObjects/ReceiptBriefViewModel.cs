using System;

namespace BusinessObjects
{
    public class ReceiptBriefViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? Price { get; set; }
        public string HasImage { get; set; }
        public DateTime DateEntered { get; set; }
        public string Status { get; set; }
    }
}
