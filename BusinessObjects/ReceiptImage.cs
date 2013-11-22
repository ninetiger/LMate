using System;

namespace BusinessObjects
{
    public class ReceiptImage
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
    }
}
