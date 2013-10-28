using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public class RentalIncome
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { set; get; }

        public DateTime YearEnded { set; get; }
        public decimal TotalIncome { set; get; }
        public decimal TotalExpenses { set; get; }
        public decimal NetRent { set; get; }

    }
}
