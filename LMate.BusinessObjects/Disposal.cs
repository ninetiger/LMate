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
        public Disposal()
        {
            Date = DateTime.Now;
        }

        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        public int ReceiptID { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public DisposalType Type { get; set; }
        public string Value { get; set; }
    }
}
