using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public class RentalIncome
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [DisplayName("Year ended 31 March")]
        public int YearEnded { get; set; }

        [DisplayName("Total income")]
        public decimal TotalIncome { get; set; }

        [DisplayName("Total expenses")]
        public decimal TotalExpenses { get; set; }

        [DisplayName("Net rents")]
        public decimal NetRent { get; set; }
    }
}
