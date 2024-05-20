using Microsoft.AspNetCore.Mvc.Rendering;
using ShoelessJoe.App.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoe.App.Models.PostModels
{
    public class PostShoeViewModel
    {
        public int ShoeId { get; set; }

        [Display(Name = "Right Shoe Size")]
        public double? RightSize { get; set; }
        [Display(Name = "Left Shoe Size")]
        public double? LeftSize { get; set; }

        [Display(Name = "Model selection")]
        [ShoelessJoeRequired]
        public int? ModelId { get; set; }
        public List<SelectListItem> ModelDropDown { get; set; }

        [Display(Name = "Manufacter selection")]
        [ShoelessJoeRequired]
        public int? ManufacterId { get; set; }
        public List<SelectListItem> ManufacterDropDown { get; set; }

        [Display(Name = "Left Shoe 1")]
        public IFormFile LeftShoeFile1 { get; set; }
        [Display(Name = "Left Shoe 2")]
        public IFormFile LeftShoeFile2 { get; set; }

        [Display(Name = "Right Shoe 1")]
        public IFormFile RightShoeFile1 { get; set; }

        [Display(Name = "Right Shoe 2")]
        public IFormFile RightShoeFile2 { get; set; }
    }
}
