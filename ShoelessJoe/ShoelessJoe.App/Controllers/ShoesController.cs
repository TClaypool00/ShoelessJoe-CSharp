using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoelessJoe.App.Models.PostModels;
using ShoelessJoe.Core.Interfaces;

namespace ShoelessJoe.App.Controllers
{
    [Authorize]
    public class ShoesController : ControllerHelper
    {
        private readonly IManufacterService _manufacterService;
        private readonly IModelService _modelService;

        public ShoesController(IManufacterService manufacterService, IModelService modelService)
        {
            _manufacterService = manufacterService;
            _modelService = modelService;
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var model = new PostShoeViewModel
            {
                ModelDropDown = await _modelService.GetModelDropDown(UserId),
                ManufacterDropDown = await _manufacterService.GetCoreManufacterDropDown(UserId)
            };

            return View(model);
        }
    }
}
