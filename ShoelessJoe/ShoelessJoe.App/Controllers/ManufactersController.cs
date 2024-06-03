using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoelessJoe.App.Models;
using ShoelessJoe.Core.Interfaces;

namespace ShoelessJoe.App.Controllers
{
    [Authorize]
    public class ManufactersController : ControllerHelper
    {
        private readonly IManufacterService _manufacterService;

        public ManufactersController(IManufacterService manufacterService)
        {
            _manufacterService = manufacterService;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ManufacterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return DisplayErrors();
            }

            if (await _manufacterService.ManufacterExistByName(model.ManufacturerName, UserId))
            {
                return BadRequest(_manufacterService.ManufacterNameExistsMessage);
            }

            var coreManufacter = Mapper.MapManufacter(model, UserId);

            coreManufacter = await _manufacterService.AddManufacterAsync(coreManufacter);

            model.ManufacturerId = coreManufacter.ManufacterId;

            return Ok(model);
        }
    }
}
