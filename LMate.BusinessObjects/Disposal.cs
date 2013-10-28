using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{

    public enum DisposalType
    {
        Sold, Disposed, Removed
    }

    public class Disposal
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { set; get; }

        public int ReceiptID { set; get; }
        public DateTime Date { set; get; }
        public string Note { set; get; }
        public DisposalType Type { set; get; }
        public string Value { set; get; }
    }
}
