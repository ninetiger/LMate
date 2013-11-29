using System.Collections.Generic;
using System.Web.Mvc;
using BusinessObjects;

namespace WebUI.Models
{
    public class ReceiptEditViewModel
    {
        public ReceiptViewModel ReceiptViewModel { get; set; }
        public IEnumerable<SelectListItem> AccountTypeSelectList { get; set; }
        public IEnumerable<SelectListItem> CurrencySelectList { get; set; }
        //public ICollection<ReceiptImage> ReceiptImageList { get; set; }
        //public ICollection<FileContentResult> FileContentResultList { get; set; }
    }
}