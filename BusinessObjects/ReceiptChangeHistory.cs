using System;

namespace BusinessObjects
{
    public class ReceiptChangeHistory
    {
        public string Id { get; set; }
        public string UpdaterId { get; set; }
        public DateTime Date { get; set; }
        public string Changes { get; set; }
        public string Details { get; set; }
    }
}
