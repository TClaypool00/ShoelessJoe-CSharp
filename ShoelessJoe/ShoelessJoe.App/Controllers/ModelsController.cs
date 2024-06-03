using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoelessJoe.App.Models.PostModels;
using ShoelessJoe.Core.Interfaces;
using ShoelessJoe.DataAccess.Services;

namespace ShoelessJoe.App.Controllers
{
    [Authorize]
    public class ModelsController : ControllerHelper
    {
        private readonly IModelService _modelService;
        private readonly IManufacterService _manufacterService;

        public ModelsController(IModelService modelService, IManufacterService manufacterService)
        {
            _modelService = modelService;
            _manufacterService = manufacterService;
        }

        [HttpGet]
        public async Task<ActionResult> GetDropDownAsync([FromQuery] int manufacturerId, int? index = null)
        {
            if (!await _manufacterService.ManufacterExistsByUserId(manufacturerId, UserId))
            {
                return Unauthorized("You do have access to this resource");
            }

            var dropDowns = await _modelService.GetModelDropDown(manufacturerId, index);

            return Ok(dropDowns);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([FromBody] PostModelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return DisplayErrors();
            }

            if (await _modelService.ModelNameExistsAsync(model.ModelName, UserId))
            {
                return BadRequest(_modelService.ModelNameExistMessage(model.ModelName));
            }

            var coreModel = Mapper.MapModel(model);

            coreModel = await _modelService.AddModelAsync(coreModel);

            model.ModelId = coreModel.ModelId;

            return Ok(model);
        }
    }
}
