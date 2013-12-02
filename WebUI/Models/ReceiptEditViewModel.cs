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
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
        //public MultiSelectList CategorySelectList { get; set; }
    }
}