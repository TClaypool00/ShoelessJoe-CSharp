using ShoelessJoe.App.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoe.App.Models
{
    public class LoginViewModel
    {
        [ShoelessJoeRequired]
        [ShoelessJoeMaxLength]
        public string Email { get; set; }

        [ShoelessJoeRequired]
        [ShoelessJoeMaxLength]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
