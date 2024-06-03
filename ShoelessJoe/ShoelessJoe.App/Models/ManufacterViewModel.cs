using ShoelessJoe.App.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoe.App.Models
{
    public class ManufacterViewModel
    {
        [Display(Name = "Manufacturer Id")]
        public int ManufacturerId { get; set; }

        [Display(Name = "Manufacturer Name")]
        [ShoelessJoeRequired]
        [ShoelessJoeMaxLength]
        public string ManufacturerName { get; set; }
    }
}
