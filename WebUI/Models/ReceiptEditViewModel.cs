using System.Linq;
using BusinessObjects;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebUI.Models
{
    public class ReceiptEditViewModel
    {
        public Receipt Receipt { get; set; }
        public List<SelectListItem> AccountTypeSelectList { get; set; }
        public List<SelectListItem> CurrencySelectList { get; set; }
        public IQueryable<ReceiptImage> ImageList { get; set; }
    }
}