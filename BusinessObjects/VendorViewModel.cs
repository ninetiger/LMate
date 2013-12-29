using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BusinessObjects
{
    public class VendorViewModel
    {
        public VendorViewModel()
        {
            Version = "NotSet";
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        [DisplayName("Vendor")]
        [StringLength(50, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string VendorName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Version { get; set; }
    }
}
