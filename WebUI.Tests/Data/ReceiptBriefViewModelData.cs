using BusinessObjects;
using System;
using System.Collections.Generic;

namespace WebUI.Tests.Data
{
    public static class ReceiptBriefViewModelData
    {
        public static ReceiptBriefViewModel GetData0()
        {
            return new ReceiptBriefViewModel()
              {
                  Id = 0,
                  Description = "d0",
                  Vendor = "v0",
                  PurchaseDate = Convert.ToDateTime("01 Jan 2011"),
                  Price = (decimal?)10.1,
                  HasImage = "Yy",
                  DateEntered = Convert.ToDateTime("01 Feb 2011"),
                  Status = "Approved"
              };
        }

        public static ReceiptBriefViewModel GetData1()
        {
            return new ReceiptBriefViewModel()
            {
                Id = 1,
                Description = "d1",
                Vendor = "v1",
                PurchaseDate = null,
                Price = null,
                DateEntered = Convert.ToDateTime("15 Mar 2011"),
                Status = "Draft"
            };
        }

        public static IEnumerable<ReceiptBriefViewModel> GetList()
        {
            return new List<ReceiptBriefViewModel> {GetData0(), GetData1()};
        }
    }
}
