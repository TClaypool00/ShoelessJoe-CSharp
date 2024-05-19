using System.ComponentModel.DataAnnotations;

namespace ShoelessJoe.App.Attributes.Validation
{
    public class ShoelessJoeRequired : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = $"{validationContext.DisplayName} is a required field";

            return base.IsValid(value, validationContext);
        }
    }
}
