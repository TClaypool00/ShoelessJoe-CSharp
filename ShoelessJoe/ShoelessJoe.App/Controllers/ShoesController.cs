using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoelessJoe.App.Models.PostModels;
using ShoelessJoe.Core.Interfaces;

namespace ShoelessJoe.App.Controllers
{
    [Authorize]
    public class ShoesController : ControllerHelper
    {
        private readonly IManufacterService _manufacterService;
        private readonly IModelService _modelService;
        private readonly IShoeService _shoeService;
        private static List<SelectListItem> _manufacterDropDown;
        private static List<SelectListItem> _modelDropDown;


        public ShoesController(IManufacterService manufacterService, IModelService modelService, IShoeService shoeService)
        {
            _manufacterService = manufacterService;
            _modelService = modelService;
            _shoeService = shoeService;
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var model = new PostShoeViewModel
            {
                ModelDropDown = await _modelService.GetModelDropDown(),
                ManufacterDropDown = await _manufacterService.GetCoreManufacterDropDown(UserId)
            };

            _manufacterDropDown = model.ManufacterDropDown;
            _modelDropDown = model.ModelDropDown;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([FromForm] PostShoeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ManufacterDropDown = _manufacterDropDown;
                model.ModelDropDown = _modelDropDown;

                return View(model);
            }

            model.ToList();

            var coreShoe = Mapper.MapShoe(model);

            coreShoe =  await _shoeService.AddShoeAsync(coreShoe);

            SetTempSuccessMessage(_shoeService.ShoeAddedMessage);

            return RedirectToAction("View", new
            {
                Id = coreShoe.ShoeId
            });

        }
    }
}
