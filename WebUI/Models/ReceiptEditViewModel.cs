using System.Collections.Generic;
using System.Web.Mvc;
using DataObjects.EntityFramework;

namespace WebUI.Models
{
    public class ReceiptEditViewModel
    {
        public Receipt Receipt { get; set; } //todo no need to include image collection
        public IEnumerable<SelectListItem> AccountTypeSelectList { get; set; }
        public IEnumerable<SelectListItem> CurrencySelectList { get; set; }
        //public ICollection<ReceiptImage> ReceiptImageList { get; set; }
        //public ICollection<FileContentResult> FileContentResultList { get; set; }
    }
}