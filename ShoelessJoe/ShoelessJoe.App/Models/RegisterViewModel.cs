using System.ComponentModel.DataAnnotations;

namespace ShoelessJoe.App.Models
{
    public class RegisterViewModel : UserViewModel
    {
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
