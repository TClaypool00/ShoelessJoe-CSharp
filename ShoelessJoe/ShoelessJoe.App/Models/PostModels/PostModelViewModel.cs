using ShoelessJoe.App.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoe.App.Models.PostModels
{
    public class PostModelViewModel
    {
        public int ModelId { get; set; }

        [Display(Name = "Model Name")]
        [ShoelessJoeRequired]
        [ShoelessJoeMaxLength]
        public string ModelName { get; set; }

        [Display(Name = "Manufacter Id")]
        [ShoelessJoeRequired]
        public int ManufacterId { get; set; }
    }
}
