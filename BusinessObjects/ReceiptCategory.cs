using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BusinessObjects
{
    public class ReceiptCategory
    {
        [Required]
        public int Id { set; get; }

        [StringLength(50, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string Type { get; set; }

        [StringLength(128, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
        public string UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Version { get; set; }

    }
}
