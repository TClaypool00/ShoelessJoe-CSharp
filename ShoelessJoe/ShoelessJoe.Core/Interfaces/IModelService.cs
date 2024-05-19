﻿using ShoelessJoe.Core.CoreModels;
using ShoelessJoe.Core.CoreModels.PartialModels;

namespace ShoelessJoe.Core.Interfaces
{
    public interface IModelService
    {
        #region Public Properties
        public string NoModelsFoundMessage { get; }
        #endregion

        #region Public Methods
        public Task<List<CoreModel>> GetModelsAsync(int? userId = null, int? index = null);

        public Task<List<CoreModelDropDown>> GetModelDropDown(int userId, int? index = null);

        public Task<CoreModel> GetModelAsync(int id);

        public Task<CoreModel> AddModelAsync(CoreModel model);

        public Task<CoreModel> UpdateModelAsync(CoreModel model, int id);

        public Task DeleteModelAsync(int id);

        public Task<bool> ModelExistsAsync(int id, int? userId = null);

        public Task<bool> ModelNameExistsAsync(string name, int userId, int? id = null);

        #region Message Methods
        public string ModelNotFoundMessage(int id);
        public string ModelNameExistMessage(string name);
        #endregion
        #endregion
    }
}