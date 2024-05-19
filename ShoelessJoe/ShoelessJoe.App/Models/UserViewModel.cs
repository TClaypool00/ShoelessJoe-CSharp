using ShoelessJoe.App.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoe.App.Models
{
    public class UserViewModel : LoginViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [ShoelessJoeRequired]
        [ShoelessJoeMaxLength]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [ShoelessJoeRequired]
        [ShoelessJoeMaxLength]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [ShoelessJoeRequired]
        [ShoelessJoeMaxLength(10)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Must be a valid phone number")]
        public string PhoneNumb { get; set; }
        public bool IsAdmin { get; set; }
    }
}
