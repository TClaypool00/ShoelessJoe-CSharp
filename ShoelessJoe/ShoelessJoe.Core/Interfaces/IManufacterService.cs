using Microsoft.AspNetCore.Mvc.Rendering;
using ShoelessJoe.Core.CoreModels;

namespace ShoelessJoe.Core.Interfaces
{
    public interface IManufacterService
    {
        #region Pubilc Properties
        public string NoManufactersFoundMessage { get; }
        public string ManufacterDeletedOKMessage {  get; }
        #endregion

        #region Public Methods
        public Task<List<CoreManufacter>> GetManufactersAsync(int? userId = null, int? index = null);

        public Task<List<SelectListItem>> GetCoreManufacterDropDown(int userId, int? index = null);

        public Task<CoreManufacter> GetGetManufacterAsync(int id);

        public Task<CoreManufacter> AddManufacterAsync(CoreManufacter manufacter);

        public Task<CoreManufacter> UpdateManufacter(CoreManufacter manufacter, int id);

        public Task DeleteManufacterAsync(int id);

        public Task<bool> ManufacterExistsById(int id);

        public Task<bool> ManufacterExistByName(string name, int userId);

        public Task<bool> ManufacterExistsByUserId(int id, int userId);

        #region Message Methods
        public string ManufacterNotFoundMessage(int id);

        public string ManufacterNameExists(string name);
        #endregion
        #endregion
    }
}
