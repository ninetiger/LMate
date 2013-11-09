using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LMate.BusinessObjects
{
    public class TaxUser
    {
        [HiddenInput(DisplayValue = false)]
        //[Key]
        public int Id { get; set; }

        //[Required]
        //[StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        //[DataType(DataType.Text)]
        [DisplayName("Your name")]
        public string Name { get; set; }
        
        //[Required]
        [DisplayName("IRD number")]
        public int IRDNumber { get; set; }

        //[Required]
        public string Address { get; set; }
    }
}
