using System.ComponentModel;
using System.Web.Mvc;

namespace BusinessObjects
{
    public class RentalIncome
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

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
