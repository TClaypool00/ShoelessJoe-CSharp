using System.ComponentModel.DataAnnotations;

namespace ShoelessJoe.App.Attributes
{
    public class ShoelessJoeMaxLength : MaxLengthAttribute
    {
        public ShoelessJoeMaxLength() : base(255)
        {

        }

        public ShoelessJoeMaxLength(int length) : base(length)
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = $"{validationContext.DisplayName} has a max length of {Length} characters";

            return base.IsValid(value, validationContext);
        }
    }
}
